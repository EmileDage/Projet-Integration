using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ce script gere un field et ces possibilités
public enum field_possibilities
{
    empty,
    agriculture,
    enclos,
    entrepot,
    mine
}

public class Field : MonoBehaviour, IBuildable
{
    [SerializeField] private Field_UI ui;
    [SerializeField] private GameObject self;
    private field_possibilities f_Type;
    
    //apparence physique
    [SerializeField] private GameObject enclos;
    [SerializeField] private GameObject entrepot;
    [SerializeField] private GameObject garden;
    [SerializeField] private GameObject mine;

    [SerializeField] private GameObject pannelInfo;



   public field_possibilities F_type { get => f_Type; set => f_Type = value; }
    public GameObject jardin { get => garden;  }
    public GameObject Enclos { get => enclos;}
    public GameObject Entrepot { get => entrepot; }
    public GameObject Mine { get => mine; }
    public GameObject PannelInfo { get => pannelInfo; set => pannelInfo = value; }

    private void Start()
    {

        f_Type = field_possibilities.empty;

        //Desactive les objets so ils apparaissent pas
        jardin.SetActive(false);
        Enclos.SetActive(false);
        Entrepot.SetActive(false);
        Mine.SetActive(false);
        PannelInfo.SetActive(false);
        //for testing; fait pop le menu_ui sans interaction
        //Interact();
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            ui.Btn_DeactivatePannelUpgrade();//empeche le joeuur de regarder l'ui de loin
            ui.Return_cursor_norm();//s'assure que le curseur revient a la normale

        }
    }



    //Called by field_ui
    //s'assure que les bonnes shits s'activent
    public void Activate_type()
    {
        switch (F_type)
        {
            case field_possibilities.agriculture:
                {
                    jardin.SetActive(true);
                    jardin.GetComponent<Garden>().Info = true;
                    jardin.GetComponent<Garden>().Info_Pannel = PannelInfo;
                    jardin.AddComponent<Garden_UI>();
                    jardin.GetComponent<Garden_UI>().Set_ref(jardin.GetComponent<Garden>()); ;


                }
                break;
            case field_possibilities.enclos:
                {
                    Enclos.SetActive(true);
                    Enclos.GetComponent<Enclos>().Info = true;
                    Enclos.GetComponent<Enclos>().Info_pannel = PannelInfo;
                }
                break;
            case field_possibilities.entrepot:
                {
                    Entrepot.SetActive(true);
                }
                break;
            case field_possibilities.mine:
                {
                    Mine.SetActive(true);

                    Mine.GetComponent<Mine>().Info = true;
                    Mine.GetComponent<Mine>().Info_Pannel = PannelInfo;//Info_Pannel de mine != null
                    Mine.AddComponent<Mine_UI>();
                    Mine.GetComponent<Mine_UI>().Set_ref(Mine.GetComponent<Mine>()); ;
                }
                break;
        }
    }
    //Called by field_ui
    //s'assure que les bonnes shits se desactivent
    public void Deactivate_type()
    {
        switch (F_type)
        {
            case field_possibilities.agriculture:
                {                   
                    Destroy(jardin.GetComponent<Garden_UI>());
                    jardin.GetComponent<Garden>().Destroy_planter();
                   jardin.SetActive(false);
                }
                break;
            case field_possibilities.enclos:
                {
                    Enclos.GetComponent<Enclos>().DestroyEnclos();
                    Enclos.SetActive(false);
                }
                break;
            case field_possibilities.entrepot:
                {
                    Entrepot.SetActive(false);
                    Entrepot.GetComponent<Entrepot>().DestroyUpgrades();
                }
                break;
            case field_possibilities.mine:
                {
                    Destroy(Mine.GetComponent<Mine_UI>());
                    Mine.GetComponent<Mine>().Destroy_planter();
                    Mine.SetActive(false);
                }
                break;
        }
    }

    public void Build()
    {
        ui.SetReference(this);
        ui.Interact();
    }
}
