using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enclos : MonoBehaviour
{
    //contient les upgrades
    private GameObject info_pannel;//recoit ref in field
    [SerializeField] private Text pannel_info;
    private bool info;//pour eviter que les pannels affichent les mauvaises informations
    [SerializeField] private GameObject abreuvoir;//ils sont pas upgrader il faut activer leur fnct OnUpgrade()
    private Abreuvoir eau;
    [SerializeField] private GameObject feeder;//ils sont pas upgrader il faut activer leur fnct OnUpgrade()
    private Food_dispenser bouffe;
    private bool deluxe_grass;
    private bool boosted_grass;// sujet a changement //double ressources animaux

    [SerializeField] private Transform[] patrolPoints; //Assigner la creature a un enclos
    [SerializeField] private Transform spawnPoint; // teleporter la creature Captured a l'enclos

    [SerializeField] private GameObject[] animaux;
    //S'assurer qu'une seule espece par enclos pour food et diete
    [SerializeField][Range(1, 10)] private int max_animal; //nombre maximal d'animaux avant que le happiness soit iompacte
    private int happiness;//happiness overall in enclos //affected by number of animals
    private int happiness_moy_ani; //happinesss moyenne des animaux

    private MyTimeManager thyme;//pour updater le bonheur and other

    public bool Info { get => info; set => info = value; }
    public GameObject Info_pannel { get => info_pannel; set => info_pannel = value; }
    public GameObject[] Animaux { get => animaux; set => animaux = value; }
    public bool Boosted_grass { get => boosted_grass; set => boosted_grass = value; }
    public Transform[] PatrolPoints { get => patrolPoints; set => patrolPoints = value; }

    private void Start()
    {
        thyme = MyTimeManager.timeInstance;
        thyme.GHourPassed += OnGHourPassed;

        happiness = 60;//starting value ?
        
        //just to be safe
        Boosted_grass = false;
        deluxe_grass = false;

        eau = abreuvoir.GetComponent<Abreuvoir>();
        bouffe = feeder.GetComponent<Food_dispenser>();
    }


    private void OnGHourPassed(object source)
    {
        //update la bouffe voir si le joueur a mis de quoi dans le contenant pour la nourriture
        bouffe.FetchFromChest();

     

        if (Animaux.Length > 0)
        {
            happiness_moy_ani = 0; // reset le calcul bonheur
            //creature behavior is the script with happiness
            foreach (GameObject animal in Animaux)
            {
                CalculateModifierAnimal(animal.gameObject.GetComponent<CreatureBehavior>());
                happiness_moy_ani += (int)animal.gameObject.GetComponent<CreatureBehavior>().GetHappiness();
            }

            happiness_moy_ani = happiness_moy_ani / Animaux.Length;
            Debug.Log("Bonheur moyen = " + happiness_moy_ani);
        }
       

        if (Info == true)
        { // pour veirifer que l'enclos est actif
          //sinon cette fonction s'active alors que exemple la mine est active
            InfoPannelTxt_enclos();
        }
    }


    public void CalculateModifierAnimal(CreatureBehavior animal)
    {
        //modifie le happiness animal unique
        //a mettre dans un loop

        //Check water

        if (eau.Qte_level >= 10)//sil y a au moins assez d'eau pour 1 animal
        { //chaque animal a beosin de 10% d'eau
            eau.Consumme();
            Debug.Log(eau.Qte_level);
            animal.ModifyHappiness(0.05);


        }else{
            animal.ModifyHappiness(-0.05);

        }


        //check food
        if (bouffe.Qte_level >= 10)//sil y a au moins assez d'eau pour 1 animal
        { //chaque animal a beosin de 10% de bouffe
            bouffe.Consumme();
            Debug.Log(bouffe.Qte_level);
            animal.ModifyHappiness(0.05);
        }
        else
        {
            animal.ModifyHappiness(-0.05);

        }

        //check comfy grass
        if (deluxe_grass) {
                animal.ModifyHappiness(0.05);
        }

        //check if enclos is overcrowded
        if (max_animal < Animaux.Length)
        {//ca descend vraiment rapidement le bonheur
            animal.ModifyHappiness((Animaux.Length - max_animal)/100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check comment les animaux sont fait pour voir comment les identifier
        //en ce moment il y a pas de tag animal
        //dans le if ca pourrait etre autre chose
        if (other.tag == "animal")
        {
            // yeet

        }
    }

    private void OnTriggerExit(Collider other)
    {//animal be escaping
        if (other.tag == "animal")
        {
            //yeet
            //check si animal a script CreatureBehavior
            //Change bool IsCaptured to false
        }
    }

    public void InfoPannelTxt_enclos()//lorsqu'il est appeler il regarde et inscrit des informations                                     
    { //utiliser cette fonction idealement pour updater le txt

        pannel_info.text = "Animals : " + Animaux.Length +
                           "\nHappiness : " + happiness_moy_ani +
                           "\nWater : " + eau.Qte_level +
                           "\nFood : " + bouffe.Qte_level;

        
    }

    public void DestroyEnclos() {
        Info = false;
        Info_pannel.SetActive(false);
    }

    #region Upgrades
    //les functions se font appeler par field_ui
    //si les fonctions se font appeler cets que le joueur a acheter l'upgrade
    //donc il faut activer l'upgrade
    //le check pour le cout est dans field_ui
    public void InfoPan_Activate()
    {
        Info_pannel.SetActive(true);
        InfoPannelTxt_enclos();
    }

    public void Auto_feeder_Activate()
    {
        bouffe.OnUpgrade();
    }

    public void Grass_d_Activate()//deluxe grass
    {
        deluxe_grass = true;
    }

    public void Water_Activate()//auto-refill water
    {
        eau.OnUpgrade();
    }

    public void Grass_b_Activate()//boosted grass
    {
        Boosted_grass = true;
    }

    #endregion
}

