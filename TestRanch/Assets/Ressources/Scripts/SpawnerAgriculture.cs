using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Collider))] "on verra" - Legault
public class SpawnerAgriculture : WildPlantSpawner, IFarmable
{
    //very important :)
    //https://twitter.com/RabbitEveryHour/status/1397930644143960066?s=20

    //cycle grandissement
    //doit etre arroser pour grandir donc si hydratation trop low pas de grandissement
    [SerializeField]private int timeToGrowHour;//combien d'heures avant que la plante soit prête a fleurir
    private bool IsGrown;

    //hydration
    //le jour doit au moins arrose la plantation adulte au moins une fois apr recolte
    //sinon moins de ressources produites
    //+ de chance de maladie
    [SerializeField] [Range(5, 50)] private int hydration_hour;//for baby and adult plant
    private Abreuvoir water_container;

    //Sante/maladie
    //en ce moment la maladie arrete la production de produit on pourra modifier ça plus tard
    [SerializeField] [Range(0, 100)] private int health;//en %
    [SerializeField] [Range(0, 100)] private int sickness_resistance;//en %
    private int sicknessLvl;//en %  0 = healthy

    //je prefere creer un autre array pour q'on puisse changer plus facilement les upgrades si on chnage d'idée pour le nombre
    //ou si chaque ressources a une upgrade différente
    [SerializeField] private bool upgrade_fertilizer;//rich ferterlizer
    [SerializeField] private Transform[] upgrade_slot;//si l'upgrade est acheter l'arbre donne plus de ressources
    private GameObject[] upgrade_produit;

    private Garden jardin;//for some FUCKING reason quand cest serialisez la reference reste apres avoir ete assigner 

    public bool Upgrade_fertilizer { get => upgrade_fertilizer; set => upgrade_fertilizer = value; }//rich fertilizer upgrade 

    public bool GrownYet { get => IsGrown; }

    public int GetSickness { get => sicknessLvl; }
    public int GetSicknessRes { get => sickness_resistance; }

    public int TimeTillGrowed { get => timeToGrowHour; }
    public Garden Jardin { get => jardin; set => jardin = value; }

    protected override void Start()
    {
        base.Start();
        sicknessLvl = 0;
        IsGrown = false;
        upgrade_produit = new GameObject[upgrade_slot.Length];
        
        if (timeToGrowHour <= 0)
        {//we want the plant to grow so never 0
            timeToGrowHour = 10;
        }

        MakeIndisponible();
        Debug.Log(Jardin);
        Jardin.UpdateInfoPannel();
    }

    public void AssignRef(Abreuvoir agua, Garden jardin_)//on pourrait mettre une fonction plus detailler ssi on veut
    {//exemple on pourrait envoyer le mesh selon le fruit ou whatever
        water_container = agua;
        Jardin = jardin_;
    }

    public void CrystalCure(int strenght) 
    {
        //utlise un crystal pour baisser la maladie //en le yeetant sur la plante ou le jardin
        sicknessLvl -= strenght;
    }

    public void GrowthCheck() {//verifie que la plante peut grandir
        if (sicknessLvl < sickness_resistance)
        { //check si plante est PAS malade
            if (water_container.Qte_level >= hydration_hour)//est ce que la plante est assez hydrate
            {
                water_container.RemoveWater(hydration_hour);
                timeToGrowHour -= 1;
                Debug.Log(timeToGrowHour + "timetogrow");

                if ((sicknessLvl - 5) <= 0)
                    sicknessLvl = 0;// 0 = perfect health
                else
                    sicknessLvl -= 5;

                if (timeToGrowHour <= 0)
                {
                    Debug.Log("plant is grown");
                    IsGrown = true;
                    Jardin.gameObject.GetComponent<Garden_UI>().CheckPendingUpgrades();
                    if (AlwaysAvailable)
                    {
                        MakeDisponible();
                    }
                    //check if hours are correct
                    if (disponibleStart < time.Hour && disponibleEnd > time.Hour)
                    {//si exemple dispo start = 2h et end = 12h
                        MakeDisponible();

                    } else if (disponibleStart > disponibleEnd) { //si exemple dispo start = 20h et end = 5h
                        if (disponibleStart < time.Hour || disponibleEnd > time.Hour) {
                            MakeDisponible();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("The plant isnt watered so it wont grow");
                sicknessLvl += 5;
            }
        }
        else {
            Debug.Log("Your growing plant is sick it will wilt !");
            timeToGrowHour++;
            sicknessLvl += 5;
        }
    }

  

    public void OnCrystalUpgrade()
    {
        sickness_resistance += 30;
        Debug.Log("SICKNESS RES "+sickness_resistance);
        Jardin.UpdateInfoPannel();
    }

    public void Deactivate_Upgrade() {
        Deactivate_Chrono();
        Upgrade_fertilizer = false;
        sickness_resistance -= 30;
    }

    public override void OnGHourPassed(object source)
    {   

        if (sickness_resistance < sicknessLvl)
        { //si la plante est malade
            Debug.Log("plant is sick");
            sicknessLvl += 5;

            if (sicknessLvl >=  100) {//la plante est morte big f
                KillAllNode();
            }
        }
        else {
            if (!IsGrown)
            {
                GrowthCheck();
            }
            else
            {

                //produce if time is correct
                if (disponibleStart == time.Hour || AlwaysAvailable)
                {
                    if (water_container.Qte_level >= hydration_hour)
                    {
                        //si la plante est hydratée elle produit des produits

                        water_container.RemoveWater(hydration_hour);
                        MakeDisponible();
                    }
                    else
                    {
                        Debug.Log("thristy plant goes to horny jail");
                        //la plante est pas hydrate so no produit for u
                        MakeIndisponible();
                        sicknessLvl += 5;
                    }

                }
                else if (disponibleEnd == time.Hour && !AlwaysAvailable)
                {
                    MakeIndisponible();
                }
            }
        }

        Jardin.UpdateInfoPannel();

    }

  
    public override void SpawnSpawner(Materiaux toSpawn)
    {     

        base.SpawnSpawner(toSpawn);
        MakeIndisponible();//when you remove this, product spawn at the beguinning even tho its supposed to be growing so no fruit

    }


   new public void  FarmIt()
    {
        GameObject loot = plante.SpawnAsObject(new ItemStack(plante, 1), this.transform);
        //Debug.Log(loot);
        
        //loot.GetComponent<WorldObjectMateriaux>().Qte = 1;
        loot.GetComponent<WorldObjectMateriaux>().Interact(GameManager.gmInstance.Joueur);
        hp--;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
