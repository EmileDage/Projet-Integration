using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_dispenser : Dispenser
{
    [SerializeField] private GameObject container;//pour upgrade tu augmnete la quantite de truc que le coffre peu contenir
    private Coffre chest;


    //discute avec david si
    //captured animal accepte fuud

    public override void Start()
    {
        base.Start();

        chest = container.GetComponent<Coffre>();
       
    }

    public void FetchFromChest( ) {//appeler dans le OnHourChange de enclos

        if (Qte_level != 100) {
            if (Qte_level == 0)
            {//cest pas bien de diviser par 0 so pour eviter ça on a un if de plus
                Qte_level += chest.GiveItemOfFonction(10, Fonctions.produits_vegetaux);
                if (Qte_level > 0)
                {//active le visuel si la mangeoire se fait remplir
                    moving_visual.SetActive(true);
                    SetLevel(Qte_level);
                }
            } else {
                //il te manque 20%
                //-> 10%/item
                //GiveItemOfFonction(20/10, fonction) -->> 2, fonction
                int cALCUL = (100 - Qte_level) / 10;
                Qte_level += chest.GiveItemOfFonction(cALCUL, Fonctions.produits_vegetaux);
                SetLevel(Qte_level);
            }
        }


    }


    public override void Empty()
    {
        moving_visual.SetActive(false);

    }

    public override void SetLevel(int pourcentage_desirer)
    {
        //change qte level with position.z
        //Highest z = -0.8
        //lowest z=-1.15
        //diff = 0.35
        if (pourcentage_desirer > 0)
        {
            //Debug.Log("Setting the water/food at " + pourcentage_desirer + "%");
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
        chest.IncreaseSize(chest.Size +2);
    }

    public void Remove_upgrade() {
        chest.IncreaseSize(chest.Size - 2);
    }

    public override void Consumme()//Drink/eat
    {
        Qte_level -= 10;
        SetLevel(Qte_level);
        FetchFromChest();
    }
}
