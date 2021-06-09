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
            pannel_info_txt.text = "Product : "+SpawnerInstance.GetComponent<SpawnMinerals>().Produit_reference.name+
                "\nAucune idée pour l'instant d'afficher quoi d'autre."+
                "\nBlah Blah Blah";
        }
        else
        {
            pannel_info_txt.text = "This is a mine";

        }

    }



    protected override void AssignSpawnerRessource(GameObject obj)
    {
        spawnerRef.GetComponent<SpawnMinerals>().AssignRR(defaultRareRock);
        base.AssignSpawnerRessource(obj);
        this.gameObject.GetComponent<Mine_UI>().CheckPendingUpgrades();
        UpdateInfoPannel();


    }

    public void OnStalactiteUpgrade() {
        //add another spawner on the plafond
        //le ui check deja si spawnerinstance est null

        second_producer = Instantiate(SpawnerInstance, spawn_stalactite);

    }

   
}