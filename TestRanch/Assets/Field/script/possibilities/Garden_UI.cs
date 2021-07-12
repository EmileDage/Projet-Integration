using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garden_UI : MonoBehaviour
{
    //garde les fonctions appeler par field_ui qui activent les upgrades specifiques
    //some stuff to keep track of upgrades and whatnot
    private Garden planter;

    //if true the upgrade linked to thoses bools werent able to be done because the spawner was null
    private bool fertilizer;
    private bool chrono;
    private bool crystal;

    public void Delete_pending_upgrades() {
        fertilizer = false;
        chrono = false;
        crystal = false;
    }


    public void Set_ref(Garden plant) {
        planter = plant;
    }

    public void CheckPendingUpgrades()
    {
        //Debug.Log("Checking pending upgrades\nChecking spawner instance");
        //Debug.Log(planter.SpawnerInstance);

        if (planter.SpawnerInstance != null) {
            if (fertilizer)
            {//il y a deja le check pour si cest null dans la fnct
                Rich_fer_Activate();
            }

            if (chrono)
            {//il y a deja le check pour si cest null dans la fnct
                Chrono_Activate();
            }

            if (crystal)
            {//il y a deja le check pour si cest null dans la fnct
                Debug.Log("Crystal");
                Crystal_Activate();
            }
        }
       

    }


    #region Upgrades
    //les functions se font appeler par field_ui
    //si les fonctions se font appeler cets que le joueur a acheter l'upgrade
    //le check pour le cout est dans field_ui

    public void Irr_sys_Activate()//irrigation system
    {//assure que les plantes ont constantly watered
        Debug.Log("irrigation Upgrade");

        planter.Upgrades[1].SetActive(true);
        planter.Water_container.OnUpgrade();

    }

    public void Rich_fer_Activate()//rich fertilizer
    {//donne plus de ressources 
        planter.Upgrades[3].SetActive(true);
        //activate visual ?
        if (planter.SpawnerInstance != null)
        {
            Debug.Log("fertilizer Upgrade");

            planter.SpawnerInstance.GetComponent<SpawnerAgriculture>().Upgrade_fertilizer = true;
            fertilizer = false;
        }
        else {
            fertilizer = true;
        }

    }

    public void Chrono_Activate()//chrono system
    {//pousse plus rapidement
        planter.Upgrades[0].SetActive(true);
        if (planter.SpawnerInstance != null)
        {
            Debug.Log("Chrono Upgrade");

            planter.SpawnerInstance.GetComponent<SpawnerAgriculture>().OnChronoUpgrade();
            chrono = false;
        }
        else {
            chrono = true;
        }


    }

    public void Crystal_Activate()//crystal fusion
    {//reduit les chances de maladie

        planter.Upgrades[2].SetActive(true);

        if (planter.SpawnerInstance != null)
        {
            Debug.Log("Crystal Upgrade");
            planter.SpawnerInstance.GetComponent<SpawnerAgriculture>().OnCrystalUpgrade();
            crystal = false;
        }
        else {
            Debug.Log("Crystal Upgrade spawnerinstance is null" );
            Debug.Log(planter.SpawnerInstance);
            crystal = true;
        }
    }

    #endregion
}

