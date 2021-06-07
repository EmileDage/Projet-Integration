using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMinerals : AbstractSpawner
{
    [SerializeField] [Range(0,100)]private int rareRockChance;//en % idelament il est set a 100 juste pour testing
    GameObject rareRock;

    //je prefere creer un autre array pour q'on puisse changer plus facilement les upgrades si on chnage d'idée pour le nombre
    //ou si chaque ressources a une upgrade différente
    [SerializeField] private bool upgrade_soil;//rich soil
    [SerializeField] private Transform[] upgrade_slot;//si l'upgrade est acheter l'arbre donne plus de ressources
    private GameObject[] upgrade_produit;

    //cest pour le pannel info
    private Text text;


    protected override void Start()
    {
        base.Start();
    }

    public void AssignRef(PlanterParent OwO)
    {

    }

    public void OnUpgradeRR() { //RR = rare rock
        rareRockChance += 25;
    }

    public void OnUpgradeSoil() {
        upgrade_soil = true;
        //check spawner instance
    }


    public override void OnGHourPassed(object source)
    {
        if (disponibleStart == time.Hour)
        {
            foreach (GameObject produit in produits)
            {
                if (produit.GetComponent<RessourceNode>().GetSpawned()) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
                {//note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                    produit.SetActive(true);
                }
            }
        }
        else if (disponibleEnd == time.Hour)
        {
            foreach (GameObject produit in produits)
            {
                produit.SetActive(false);
            }
        }
    }

    public override void SpawnProduce()
    {
        if (UnityEngine.Random.Range(0, 100) >= rareRockChance) {
            //spawn 1 rare rock
            //codebeep boop
            for (int a = 0; a < produit_spawn.Length; a++)
            {
                if (produits[a] == null)
                {
                    produits[a].GetComponent<RessourceNode>().SetSpawnedTrue();
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
        Debug.Log("All products on this spawner are destroyed");
    }

    public Text InfoPannelMine()
    {

        text.text = "Hello world";

        return text;
    }

    public override void SpawnSpawner(Materiaux toSpawn)
    {
        if(toSpawn.Funct.Equals(Fonctions.mineraux))
            base.SpawnSpawner(toSpawn);
    }
}
