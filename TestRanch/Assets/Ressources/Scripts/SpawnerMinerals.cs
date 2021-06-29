using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerMinerals : SimpleSpawner
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
        base.Start();
        upgrade_produit = new GameObject[upgrade_slot.Length];
    }

    public void AssignRR_ref(GameObject RR)//pour si plus tard selon le type de roche tu obtient une variante rare quand tu assigne
    {
        rareRock_ref = RR;
    }

    public void OnUpgradeRR() { //RR = rare rock
        rareRockChance += 25;
    }

    public void OnUpgradeSoil() 
    {

        upgrade_soil = true;
        foreach (SimpleNode node in produits)
        {
            node.Yield *= 2;
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


}
