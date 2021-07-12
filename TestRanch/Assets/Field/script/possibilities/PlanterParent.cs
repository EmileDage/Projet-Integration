using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ce script gere une machine qui selon ce qui lui est donner plante une ressource dans un field
[RequireComponent(typeof(Collider))]
public abstract class PlanterParent : MonoBehaviour
{
    [SerializeField] private GameObject[] upgrades;
    [SerializeField] protected Text pannel_info_txt;
    private GameObject spawnerInstance;
    [SerializeField] protected Fonctions type_product;

    protected Materiaux produit;

    protected GameObject info_Pannel;//recoit ref in field 
    protected bool info;//pour eviter que les pannels affichent les mauvaises informations
    protected MyTimeManager thyme;
    [SerializeField] private Transform spawn;

    public GameObject Info_Pannel { get => info_Pannel; set => info_Pannel = value; }
    public bool Info { get => info; set => info = value; }
    public Text Pannel_info_txt { get => pannel_info_txt; set => pannel_info_txt = value; }
    public GameObject[] Upgrades { get => upgrades; }
    public GameObject SpawnerInstance { get => spawnerInstance; set => spawnerInstance = value; }

    protected virtual void Start()
    {
        thyme = MyTimeManager.timeInstance;
        thyme.GHourPassed += OnGHourPassed;
      

        foreach (GameObject go in Upgrades)
            go.SetActive(false);

    }

    public virtual void Destroy_planter()
    {
        Info_Pannel.SetActive(false);
        Info = false;
        //deactive visuel upgrades
        foreach (GameObject go in Upgrades)
            go.SetActive(false);
        //deactive upgrades

    }

    public virtual void OnGHourPassed(object source)
    {
        if (Info == true)
        { // pour veirifer que l'obj est actif
            if (info_Pannel.activeSelf ==true) {
                UpdateInfoPannel();
            }
        }
    }
    public abstract void UpdateInfoPannel();

    protected virtual void AssignSpawnerRessource(Materiaux inMat) 
    {
        SpawnerInstance = Instantiate(inMat.Spawner, spawn.position, spawn.rotation); 
    }

    private void OnCollisionEnter(Collision collision)//erreur quand fruit est lancer sur mine
    {
        if (spawnerInstance == null) {//permet eviter une erreur
            if (collision.gameObject.CompareTag("produit"))//worldobject
            {
                produit = collision.gameObject.GetComponent<WorldObjectMateriaux>().Item();

                if (produit != null)
                {
                    if (produit.Funct.Equals(type_product))
                    {//arrete une erreur dont remove 
                        AssignSpawnerRessource(produit);
                        collision.gameObject.GetComponent<WorldObject>().DecrementeQte();
                    }
                    else
                        Debug.Log("The type is incorrect not spawning spawner");

                }
            }
        }
       
    }

    public void InformationPannel_Activate()
    {

        if (info_Pannel != null)
        {
            info_Pannel.SetActive(true);
            UpdateInfoPannel();
        }
        else { 
            Debug.Log("nope");

        }


    }
}
