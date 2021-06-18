using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerAgriculture : AbstractSpawner, IFarmable
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
    [SerializeField] private Abreuvoir water_container;

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

    [SerializeField] private Garden jardin;//for some FUCKING reason quand cest serialisez la reference reste apres avoir ete assigner 

    public bool Upgrade_fertilizer { get => upgrade_fertilizer; set => upgrade_fertilizer = value; }//rich fertilizer upgrade 

    public bool GrownYet { get => IsGrown; }

    public int GetSickness { get => sicknessLvl; }
    public int GetSicknessRes { get => sickness_resistance; }

    public int TimeTillGrowed { get => timeToGrowHour; }

    protected override void Start()
    {
        sicknessLvl = 0;
        IsGrown = false;
        upgrade_produit = new GameObject[upgrade_slot.Length];

        Available = false;
        if (timeToGrowHour <= 0)
        {//we want the plant to grow so never 0
            timeToGrowHour = 10;
        }

        if (produit_reference != null)
        {
            produits = new GameObject[produit_spawn.Length];
            time = MyTimeManager.timeInstance;
            time.GHourPassed += OnGHourPassed;

            for (int i = 0; i < produit_spawn.Length; i++)
            {
                produits[i] = Instantiate(Produit_reference, produit_spawn[i]);
                produits[i].tag = "produit";
                produits[i].AddComponent<RessourceNode>();
                produits[i].GetComponent<RessourceNode>().SetupNode(this);
                produits[i].name = "Node" + i;

            }

            if (upgrade_fertilizer)
            {
                for (int i = 0; i < upgrade_produit.Length; i++)
                {
                    upgrade_produit[i] = Instantiate(Produit_reference, upgrade_slot[i]);
                    upgrade_produit[i].tag = "produit";
                    upgrade_produit[i].AddComponent<RessourceNode>();
                    upgrade_produit[i].GetComponent<RessourceNode>().SetupNode(this);
                    upgrade_produit[i].name = "Node" + i;

                }
            }
        }

        Despawn();

    }

    public void AssignRef(Abreuvoir agua, GameObject fruit, Garden jardin_)//on pourrait mettre une fonction plus detailler ssi on veut
    {//exemple on pourrait envoyer le mesh selon le fruit ou whatever
        water_container = agua;
        produit_reference = fruit;
        jardin = jardin_;
        Debug.Log(jardin);
    }

    public void CrystalCure(int strenght) {
        //utlise un crystal pour baisser la maladie
        sicknessLvl -= strenght;

    }

    public void GrowthCheck() {//verifie que la plante peut grandir
        Debug.Log("Current growth : " + timeToGrowHour);

        if (sicknessLvl < sickness_resistance)
        { //check si plante est PAS malade
            if (water_container.Qte_level >= hydration_hour)//est ce que la plante est assez hydrate
            {
                water_container.RemoveWater(hydration_hour);
                timeToGrowHour -= 1;
                Debug.Log("Growing : " + timeToGrowHour);

                if ((sicknessLvl - 5) <= 0)
                    sicknessLvl = 0;// 0 = perfect health
                else
                    sicknessLvl -= 5;

                if (timeToGrowHour <= 0)
                {
                    IsGrown = true;
                    Available = true;
                    //check if hours are correct
                    if (disponibleStart < time.Hour && disponibleEnd > time.Hour)
                    {//si exemple dispo start = 2h et end = 12h
                        Spawn();

                    } else if (disponibleStart > disponibleEnd) { //si exemple dispo start = 20h et end = 5h
                        if (disponibleStart < time.Hour || disponibleEnd > time.Hour) {
                            Spawn();
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
    }

    public override void OnGHourPassed(object source)
    {   

        if (sickness_resistance < sicknessLvl)
        { //si la plante est malade
            sicknessLvl += 5;
            //destroy one at random ?

            if (sicknessLvl >=  100) {//la plante est morte big f
                DestroyAll();
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
                if (disponibleStart == time.Hour)
                {
                    if (water_container.Qte_level >= hydration_hour)
                    {
                        //si la plante est hydratée elle produit des produits

                        water_container.RemoveWater(hydration_hour);
                        Spawn();
                        Available = true;
                    }
                    else
                    {
                        //la plante est pas hydrate so no produit for u
                        Despawn();
                        sicknessLvl += 5;
                    }

                }
                else if (disponibleEnd == time.Hour)
                {
                    Despawn();
                    Available = false;
                }
            }
        }

        jardin.UpdateInfoPannel();

        Debug.Log("Product are active "+ (produits[0].activeSelf));

    }


    protected override  void Spawn() {//pour faciliter la lecture du code jai mis ça en fonction       
        Debug.Log("Spawn Fnct");
        
        base.Spawn();

        if (upgrade_fertilizer)
        {
            foreach (GameObject produit in upgrade_produit)
            {
                if (produit.GetComponent<RessourceNode>().GetSpawned())
                {
                    produit.SetActive(true);
                }
            }
        }

    }

    protected override void Despawn()
    {
        base.Despawn();

        if (upgrade_fertilizer)
        {
            foreach (GameObject produit in upgrade_produit)
            {
                if (produit != null) {
                    if (produit.GetComponent<RessourceNode>().GetSpawned())
                    {
                        produit.SetActive(false);
                    }
                }
               
            }
        }

  
    }

    public override void SpawnProduce()
    {
        Debug.Log("SpawnProduce()");
        base.SpawnProduce();

        if (upgrade_fertilizer) {
            for (int a = 0; a < upgrade_produit.Length; a++)
            {//on ne sait pas si dans le futur le nmbr de slot sur upgrade seront egal a ceux regulier so on fait une boucle separe
                if (upgrade_produit[a] == null)
                {
                    upgrade_produit[a].GetComponent<RessourceNode>().SetSpawnedTrue();
                }
            }  
        }

        Despawn();//when you remove this, product spawn at the beguinning even tho its supposed to be growing so no fruit

    }

    public override void DestroyAll()
    {
        base.DestroyAll();

        if (upgrade_fertilizer) {
            for (int a = 0; a < upgrade_produit.Length; a++)
            {
                upgrade_produit[a].GetComponent<RessourceNode>().KillNode();
            }
        }

        Debug.Log("All products on this spawner are destroyed");
    }


    public override void SpawnSpawner(Materiaux toSpawn)
    {     

        base.SpawnSpawner(toSpawn);
        Despawn();//when you remove this, product spawn at the beguinning even tho its supposed to be growing so no fruit

    }

    public void FarmIt()
    {
        //  x////x

    }


}
