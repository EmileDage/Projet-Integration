using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ce script gere une machine qui selon ce qui lui est donner plante une ressource dans un field
[RequireComponent(typeof(Collider))]
public abstract class PlanterParent : MonoBehaviour
{
    [SerializeField] protected GameObject spawnerRef;
    [SerializeField] private GameObject[] upgrades;
    [SerializeField] protected Text pannel_info_txt;
    private GameObject spawnerInstance;
    [SerializeField] protected Fonctions type_product;



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

    public void Destroy_planter()
    {
        Info_Pannel.SetActive(false);
        Info = false;
    }

    private void OnGHourPassed(object source)
    {
        Debug.Log("HourPassed PlanterParent");

        if (Info == true)
        { // pour veirifer que l'obj est actif
            if (info_Pannel.activeSelf ==true) {
                UpdateInfoPannel();
            }
        }
    }
    public abstract void UpdateInfoPannel();

    protected virtual void AssignSpawnerRessource(GameObject inObj) 
    {
        SpawnerInstance = Instantiate(spawnerRef, spawn);
        SpawnerInstance.GetComponent<AbstractSpawner>().SpawnSpawner(inObj.GetComponent<WorldObjectMateriaux>().Item()) ;
    }

    private void OnCollisionEnter(Collision collision)//erreur quand fruit est lancer sur mine
    {
        if (collision.gameObject.CompareTag("produit")) 
        {           
            Materiaux inObj = collision.gameObject.GetComponent<WorldObjectMateriaux>().Item();

            if (inObj != null) {
                if (inObj.Funct.Equals(type_product)) {//arrete une erreur dont remove 
                    AssignSpawnerRessource(collision.gameObject);

                }else
                    Debug.Log("The type is incorrect not spawning spawner");

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
