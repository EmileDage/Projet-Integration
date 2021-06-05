using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abreuvoir : Dispenser, IWaterable
{

    public void Watering() {
        //joueur appelle cette fonction pour vider son bucket d'eau
        
        //check if bucket d'eau
        SetLevel(100);
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
            Qte_level -= 10;
            SetLevel(Qte_level);
        }
    }

    public void RemoveWater(int removed) { //pour plante pour plus de precision       
                                           //normalement cette fonction ne check pas s'il y a assez d'eau
        if (upgrade == true)
        {
            Debug.Log("Plant do be taking a sip but water aint going down cuz of that upgrade");
        }
        else
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


