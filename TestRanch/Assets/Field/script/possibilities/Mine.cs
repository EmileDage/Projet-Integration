using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : PlanterParent
{
    //stalactite upgrades
    [SerializeField] private Transform spawn_stalactite;
    private GameObject second_producer;
    [SerializeField] private GameObject defaultRareRock;


    protected override  void Start()
    {
        base.Start();
        type_product = Fonctions.mineraux;

        defaultRareRock.SetActive(false);
    }
    public override void UpdateInfoPannel()
    {
        if (SpawnerInstance != null)
        {
            pannel_info_txt.text = "Product : " + produit.Nom +
                "\nChance de roche rare :" + SpawnerInstance.GetComponent<SpawnMinerals>().RRChance+ "%";
        }
        else
        {
            pannel_info_txt.text = "This is a mine";

        }

    }



    protected override void AssignSpawnerRessource(GameObject obj)
    {
        spawnerRef.GetComponent<SpawnMinerals>().AssignRR_ref(defaultRareRock);
        base.AssignSpawnerRessource(obj);
        this.gameObject.GetComponent<Mine_UI>().CheckPendingUpgrades();
        UpdateInfoPannel();      
        Debug.Log(produit  + " assignspawnerressource mine");
    }

    public void OnStalactiteUpgrade() {
        //add another spawner on the plafond
        //le ui check deja si spawnerinstance est null

        second_producer = Instantiate(spawnerRef, spawn_stalactite);
        Debug.Log(produit);
        second_producer.GetComponent<AbstractSpawner>().SpawnSpawner(produit) ;

    }


}