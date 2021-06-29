using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMinerals : AbstractSpawner
{
    [SerializeField] [Range(0,100)]private int rareRockChance;//en % idelament il est set a 100 juste pour testing
    [SerializeField]private GameObject rareRock_ref;//reference a ce quon veut comme rare rock
    private GameObject rareRock;//Cest l'instance qui est dans la scene
    [SerializeField] private Transform rareRock_spawn;//it seems easier to just add a special spot for the rare rock

    //je prefere creer un autre array pour q'on puisse changer plus facilement les upgrades si on chnage d'idée pour le nombre
    //ou si chaque ressources a une upgrade différente
    [SerializeField] private bool upgrade_soil;//rich soil
    [SerializeField] private Transform[] upgrade_slot;//si l'upgrade est acheter la roche donne plus de ressources
    private GameObject[] upgrade_produit;

    //cest pour le pannel info
    private Text text;

    public int RRChance { get => rareRockChance; }

    protected override void Start()
    {
        upgrade_produit = new GameObject[upgrade_slot.Length];

        /*if (produit_reference != null)
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

            if (upgrade_soil) {
                for (int i = 0; i < upgrade_produit.Length; i++)
                {//J'utilise pas la même boucle que pour les produits regulier cuz maybe on changera le nmbr de spawn pour les upgrades avec l'upgrade
                    upgrade_produit[i] = Instantiate(Produit_reference, upgrade_slot[i]);
                    upgrade_produit[i].tag = "produit";
                    upgrade_produit[i].AddComponent<RessourceNode>();
                    upgrade_produit[i].GetComponent<RessourceNode>().SetupNode(this);
                    upgrade_produit[i].name = "Node" + i;

                }
            }
        }

        //this is the setup for rare rock
        if (rareRock_ref != null) {
            rareRock = Instantiate(rareRock_ref, rareRock_spawn);
            rareRock.tag = "produit";
            rareRock.AddComponent<RessourceNode>();
            rareRock.GetComponent<RessourceNode>().SetupNode(this);

        }*/




        //check if hours are correct
        if (disponibleStart < time.Hour && disponibleEnd > time.Hour)
        {//si exemple dispo start = 2h et end = 12h
            MakeDisponible();
        }
        else if (disponibleStart > disponibleEnd)
        { //si exemple dispo start = 20h et end = 5h
            if (disponibleStart < time.Hour || disponibleEnd > time.Hour)
            {
                MakeDisponible();
            }
            else
            {
                MakeIndisponible();
            }
        }
        else
        {
            MakeIndisponible();
        }



    }

    public void AssignRR_ref(GameObject RR)//pour si plus tard selon le type de roche tu obtient une variante rare quand tu assigne
    {
        rareRock_ref = RR;
    }

    public void OnUpgradeRR() { //RR = rare rock
        rareRockChance += 25;
    }

    public void OnUpgradeSoil() {

        upgrade_soil = true;

        foreach (GameObject produit in upgrade_produit)
        {
         //       if (produit.GetComponent<RessourceNode>().GetSpawned()) 
                {//note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                    produit.SetActive(true);
                }
            }
        
    }


    public override void OnGHourPassed(object source)
    {

        base.OnGHourPassed(source);
    }

    protected override void MakeDisponible()
    {
        base.MakeDisponible();

        if (upgrade_soil)
        {
            foreach (GameObject produit in upgrade_produit)
            {//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
                //if (produit.GetComponent<RessourceNode>().GetSpawned()) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
                {//note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                    produit.SetActive(true);
                }
            }
        }

        int random = UnityEngine.Random.Range(0, 100);

        if (random <= rareRockChance)
        {//si la chance de random est plus grande rare rock spawn
            rareRock.SetActive(true);
        }
        else { 
            rareRock.SetActive(false);
        }

    }

    protected override void MakeIndisponible()
    {
        base.MakeIndisponible();
        rareRock.SetActive(false);

        if (upgrade_soil)
        {//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
            foreach (GameObject produit in upgrade_produit)
            {
                if (produit != null) {
                  //  if (produit.GetComponent<RessourceNode>().GetSpawned()) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
                    { //note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                        produit.SetActive(false);
                    }
                }
            }
        }
    }


    /*public override void SpawnProduce()
    {
        //base.SpawnProduce();

        if (UnityEngine.Random.Range(0, 100) <= rareRockChance) {//si la chance de random est plus grande rare rock spawn
            rareRock.GetComponent<RessourceNode>().SetSpawnedTrue();
        }

        if (upgrade_soil)
        {
            for (int a = 0; a < upgrade_produit.Length; a++)//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
            {
                if (upgrade_produit[a] == null)
                {
                    upgrade_produit[a].GetComponent<RessourceNode>().SetSpawnedTrue();
                }
            }
        }
    }*/

    public override void KillAllNode()
    {
        for (int a = 0; a < produits.Count; a++)
        {
           // produits[a].GetComponent<RessourceNode>().KillNode();
        }

        if (upgrade_soil)
        {
            for (int a = 0; a < upgrade_produit.Length; a++)//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
            {
                if (upgrade_produit[a] == null)
                {
                  //  upgrade_produit[a].GetComponent<RessourceNode>().KillNode();
                }
            }
        }

       // rareRock.GetComponent<RessourceNode>().KillNode();
        Debug.Log("All products on this spawner are destroyed");


    }


    public override void SpawnSpawner(Materiaux toSpawn)
    {
            base.SpawnSpawner(toSpawn);
    }
}
