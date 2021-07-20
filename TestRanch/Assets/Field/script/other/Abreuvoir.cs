using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abreuvoir : Dispenser, IWaterable
{
    [SerializeField] private Garden garden_ref;


    public void Watering() {
        //joueur appelle cette fonction pour vider son bucket d'eau

        //check if bucket d'eau
        Qte_level = 100;
        SetLevel(Qte_level);

        if (enclos_ref != null) {
            if (enclos_ref.Info == true)
            { // pour veirifer que l'enclos est actif
              //sinon cette fonction s'active alors que exemple la mine est active
                Debug.Log("Update info pannel water value");
                enclos_ref.InfoPannelTxt_enclos();
            }
        } else if (garden_ref != null) {
            if (garden_ref.Info == true)
            { 
                garden_ref.UpdateInfoPannel();
            }

        }
        
    }

    public override void Empty()
    {
        moving_visual.SetActive(false);
    }

    public override void Consumme()//Drink/eat
    {
        if (upgrade == true)
        {
            Debug.Log("Taking a sip but water aint going down cuz of that upgrade");
        }
        else
        {
            Qte_level -= qteToConsume;
            SetLevel(Qte_level);
        }
    }

    public void RemoveWater(int removed) { //pour plante pour plus de precision       
                                           //normalement cette fonction ne check pas s'il y a assez d'eau
        if (!upgrade)
        {
            Qte_level -= removed;
            SetLevel(Qte_level);
        }

    }

    public override void SetLevel(int pourcentage_desirer)
    {
        //change qte level with position.z
        //Highest z = -0.8
        //lowest z=-1.15
        //diff = 0.35
        if (pourcentage_desirer > 0)
        {
            //calcule selon le pourcentage desire d'eau dans le bucket et met le visuel a la bonne hauteur
            temp.z = (float)((0.35f * pourcentage_desirer) / 100 - 1.15f);
            moving_visual.transform.localPosition = temp;
        }
        else
        {
            Empty();
        }

    }

    public override void OnUpgrade()
    {
        Debug.Log("Water container/abreuvoir has been upgraded");
        if (Qte_level <= 0) {
            moving_visual.SetActive(true);
        }

        Qte_level = 100;
        SetLevel(Qte_level);
        upgrade = true;
    }

}


