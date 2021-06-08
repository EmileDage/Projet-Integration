using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMinerals : MotherSpawner
{
    [SerializeField] [Range(0,100)]private int rareRockChance;//en % idelament il est set a 100 juste pour testing
    [SerializeField]private GameObject rareRock;
    [SerializeField] private Transform rareRock_spawn;//it seems easier to just add a special spot for the rare rock

    //je prefere creer un autre array pour q'on puisse changer plus facilement les upgrades si on chnage d'idée pour le nombre
    //ou si chaque ressources a une upgrade différente
    [SerializeField] private bool upgrade_soil;//rich soil
    [SerializeField] private Transform[] upgrade_slot;//si l'upgrade est acheter la roche donne plus de ressources
    private GameObject[] upgrade_produit;

    //cest pour le pannel info
    private Text text;


    protected override void Start()
    {
        base.Start();

        //this is the setup for rare rock
        if (rareRock != null) {
            rareRock = Instantiate(rareRock, rareRock_spawn);
            rareRock.tag = "produit";
            rareRock.AddComponent<RessourceNode>();
            rareRock.GetComponent<RessourceNode>().SetupNode(this);
        }
       
    }

    public void AssignRR(GameObject RR)//pour si plus tard selon le type de roche tu obtient une variante rare quand tu assigne
    {
        rareRock = RR;
    }

    public void OnUpgradeRR() { //RR = rare rock
        rareRockChance += 25;
    }

    public void OnUpgradeSoil() {
       
        upgrade_soil = true;

    }


    public override void OnGHourPassed(object source)
    {
        Debug.Log("HourPassed SpawnerMine");

        if (disponibleStart == time.Hour)
        {
            Spawn();        
        }
        else if (disponibleEnd == time.Hour)
        {
            Despawn();         
        }
    }

    protected override void Spawn()
    {
        base.Spawn();

        int random = UnityEngine.Random.Range(0, 100);

        if ( random <= rareRockChance)
        {//si la chance de random est plus grande rare rock spawn
            Debug.Log("Rare rock has spawn !" +random+"<="+rareRockChance);
            rareRock.SetActive(true);
        }

        if (upgrade_soil)
        {
            foreach (GameObject produit in upgrade_produit)
            {//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
                if (produit.GetComponent<RessourceNode>().GetSpawned()) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
                {//note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                    produit.SetActive(true);
                }
            }
        }


    }

    protected override void Despawn()
    {
        base.Despawn();

        rareRock.SetActive(false);


        if (upgrade_soil)
        {//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
            foreach (GameObject produit in upgrade_produit)
            {
                produit.SetActive(false);
            }
        }
    }


    public override void SpawnProduce()
    {
        base.SpawnProduce();

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
    }

    public override void DestroyAll()
    {
        for (int a = 0; a < produits.Length; a++)
        {
            produits[a].GetComponent<RessourceNode>().KillNode();
        }

        if (upgrade_soil)
        {
            for (int a = 0; a < upgrade_produit.Length; a++)//peut etre dans le futur (upgradeslot != produit spawn), donc je ne les met pas dans la meme boucle pour cela
            {
                if (upgrade_produit[a] == null)
                {
                    upgrade_produit[a].GetComponent<RessourceNode>().KillNode();
                }
            }
        }

        rareRock.GetComponent<RessourceNode>().KillNode();
        Debug.Log("All products on this spawner are destroyed");


    }


    public override void SpawnSpawner(Materiaux toSpawn)
    {
        //if(toSpawn.Funct.Equals(Fonctions.mineraux)) //on check avant dans la collision sinon bug and dats sad
            base.SpawnSpawner(toSpawn);
    }
}
