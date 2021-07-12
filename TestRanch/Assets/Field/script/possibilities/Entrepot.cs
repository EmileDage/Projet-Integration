using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrepot : MonoBehaviour
{
    //creer un script chest 
    //il donne les fonctionnalites de retrieve et give a ces heritiers
    //keep track of inventory inside too
    //ce script et ces upgardes devrait heriter de chest

    //contient les upgrades
    [SerializeField] private GameObject[] upgrades;//meme si cest un array il faut quil y ait 3 upgrades

    private void Start()
    {
        foreach (GameObject a in upgrades)
            a.gameObject.SetActive(false);
    }


    #region Upgrades
    //les functions se font appeler par field_ui
    //si les fonctions se font appeler cets que le joueur a acheter l'upgrade
    //le check pour le cout est dans field_ui
    public void Chest1_Activate()
    {
        upgrades[0].SetActive(true);
    }

    public void Chest2_Activate()
    {
        upgrades[1].SetActive(true);
    }

    public void Chest3_Activate()
    {
        upgrades[2].SetActive(true);
    }

    internal void DestroyUpgrades()
    {
        foreach (GameObject a in upgrades)
            a.gameObject.SetActive(false);
    }
    #endregion



}
