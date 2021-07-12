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
                "\nChance de roche rare :" + SpawnerInstance.GetComponent<SpawnerMinerals>().RRChance+ "%";
        }
        else
        {
            pannel_info_txt.text = "This is a mine";

        }

    }

    public override void Destroy_planter()
    {
        //deactive upgrades
        if (SpawnerInstance != null)
        {
            SpawnerInstance.GetComponent<SpawnerMinerals>().Remove_UPGRADES();
        }
        else
        {
            this.gameObject.GetComponent<Mine_UI>().Delete_pending_upgrades();
        }

        if (second_producer != null) { 
            second_producer.SetActive(false);
        }

        base.Destroy_planter();

        Destroy(this.gameObject.GetComponent<Mine_UI>());
        this.gameObject.SetActive(false);
    }

    protected override void AssignSpawnerRessource(Materiaux inMat)
    {
        
        base.AssignSpawnerRessource(inMat);
        second_producer = Instantiate(inMat.Spawner, spawn_stalactite);
        second_producer.SetActive(false);
        this.gameObject.GetComponent<Mine_UI>().CheckPendingUpgrades();
        UpdateInfoPannel();      
        
    }

    public void OnStalactiteUpgrade() {
        //add another spawner on the plafond
        //le ui check deja si spawnerinstance est null
        second_producer.SetActive(true);
    }


}