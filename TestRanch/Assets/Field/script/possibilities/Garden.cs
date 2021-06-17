using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ce script  met dans la terre un spawner 
//ca peu etre un spawner agriculutre
//ca peu etre un spawner mine

public class Garden : PlanterParent, IFarmable
{

    bool tilled = false;
     //agriculture
    [SerializeField] private Abreuvoir water_container;//irrigation

    protected override void Start()
    {
        base.Start();

        type_product = Fonctions.produits_vegetaux;


        //Until tilled is implemented
        tilled = true;
    }

    public Abreuvoir Water_container { get => water_container; set => water_container = value; }

    public override void UpdateInfoPannel()
    {
        if (SpawnerInstance != null)
        {
            if (SpawnerInstance.GetComponent<SpawnerAgriculture>().GrownYet)
            {
                pannel_info_txt.text = "Product : " + SpawnerInstance.GetComponent<SpawnerAgriculture>().Produit_reference.name +
                    "\nAucune idée pour l'instant d'afficher quoi d'autre." +
                    "\nBlah Blah Blah";
            }
            else
                pannel_info_txt.text = "ITS GROWING :)" +
                    "\nSickness :" + SpawnerInstance.GetComponent<SpawnerAgriculture>().GetSickness +
                     "\nSickness Resistance :" + SpawnerInstance.GetComponent<SpawnerAgriculture>().GetSicknessRes +
                     "\nTime till mature :" + SpawnerInstance.GetComponent<SpawnerAgriculture>().TimeTillGrowed;
        }
        else
        {
            pannel_info_txt.text = "This is a garden";

        }
    }

 

    private void TillEarth(Player joueur) {
        tilled = true;
        //play animation
        //play sound
        //play particle effect
        //add random minerals to player
    }

    protected override void AssignSpawnerRessource(GameObject obj)
    {
        if (tilled)
        {
            spawnerRef.GetComponent<SpawnerAgriculture>().AssignRef(Water_container, obj);

            base.AssignSpawnerRessource(obj);
            this.gameObject.GetComponent<Garden_UI>().CheckPendingUpgrades();
            UpdateInfoPannel();
        }
        else
            Debug.Log("Field is not tilled so you can't plant");
    }

    public void FarmIt()
    {
        //OwO
    }



  


}
