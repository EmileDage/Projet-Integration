using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractSpawner : MonoBehaviour
{
    [SerializeField]protected GameObject produit_reference; //IL FAUT GARDER SERIALIZED POUR ASSIGNER LA RESSOURCES POUR  LES SPAWNER SAUVAGES
    protected GameObject[] produits;

    [SerializeField] [Range(0, 23)] protected int timeToRespawn;
    [SerializeField] [Range(0, 23)] protected int disponibleStart; //si on veut que l'objet soit collectable seulement pendant une période de la journee
    [SerializeField] [Range(0, 23)] protected int disponibleEnd;
    [SerializeField] protected Transform[] produit_spawn;

    private bool available;

    protected MyTimeManager time;
    public GameObject Produit_reference { get => produit_reference; set => produit_reference = value; }

    public int TimeToRespawnRef{ get => timeToRespawn; }
    public bool Available { get => available; set => available = value; }

    protected virtual void Start() {//a mettre dans start set up les trucs de base

        if (produit_reference != null) 
        { 
            produits = new GameObject[produit_spawn.Length];
            time = MyTimeManager.timeInstance;
            time.GHourPassed += OnGHourPassed;

            for (int i = 0; i < produit_spawn.Length; i++)
            {
                produits[i] = Instantiate(Produit_reference, produit_spawn[i]);
                produits[i].tag = "produit";
                produits[i].AddComponent<RessourceNode>();
                produits[i].GetComponent<RessourceNode>().SetupNode(this);
                produits[i].name = "Node" + i;
                
            }
        }
        //check if hours are correct
        if (disponibleStart < time.Hour && disponibleEnd > time.Hour)
        {//si exemple dispo start = 2h et end = 12h
            Spawn();
        }
        else if (disponibleStart > disponibleEnd)
        { //si exemple dispo start = 20h et end = 5h
            if (disponibleStart < time.Hour || disponibleEnd > time.Hour)
            {
                Spawn();
            }
            else {
                Despawn();
            }
        }
        else {
            Despawn();
        }
    }

    public Materiaux SpawnedMat() {
        return this.produit_reference.GetComponent<WorldObjectMateriaux>().Item();
    }

    public virtual void OnGHourPassed(object source) {

        if (disponibleStart == time.Hour)
        {
            Spawn();
            Available = true;
        }
        else if (disponibleEnd == time.Hour)
        {
            Despawn();
            Available = false;
        }
    }

    protected virtual void Spawn() {
        
        foreach (GameObject produit in produits)
        {
            if (produit.GetComponent<RessourceNode>().GetSpawned()) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
            { //note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                produit.SetActive(true);
            }
        }
    }

    protected virtual void Despawn()
    {
        foreach (GameObject produit in produits)
        {
            produit.SetActive(false);
        }
    }

    public virtual void SpawnProduce() {
        for (int a = 0; a < produit_spawn.Length; a++)
        {
            if (produits[a] == null)
            {
                produits[a].GetComponent<RessourceNode>().SetSpawnedTrue();
            }
        }
    }

    //exemple arbre est malade dont tous ces fruits pourrissent/roche rot idk
    public virtual  void DestroyAll()
    {
        for (int a = 0; a < produits.Length; a++)
        {
            produits[a].GetComponent<RessourceNode>().KillNode();
        }
        
    }

    public virtual void OnChronoUpgrade()
    {
        timeToRespawn = timeToRespawn / 2;//on peut le changer plus tard si cest pas balancer ou whatever

        for (int i = 0; i < produit_spawn.Length; i++)
        {
            produits[i].GetComponent<RessourceNode>().OnChronoUpgrade(timeToRespawn);
        }

    }

    //change destroy 1 pour destroy1atrandom, destroyone deplace vers le ressourcenode
    public void DestroyOneAtRandom()
    {
        produits[UnityEngine.Random.Range(0, produits.Length)].GetComponent<RessourceNode>().KillNode();//yark c lette
    }

    public virtual void SpawnSpawner(Materiaux toSpawn)
    {
        Debug.Log(toSpawn);
        produit_reference = toSpawn.ItemWorldObject;
        produits = new GameObject[produit_spawn.Length];
        time = MyTimeManager.timeInstance;
        time.GHourPassed += OnGHourPassed;

        for (int i = 0; i < produit_spawn.Length; i++)
        {
            produits[i] = Instantiate(Produit_reference, produit_spawn[i]);
            produits[i].tag = "produit";
            produits[i].AddComponent<RessourceNode>();
            produits[i].GetComponent<RessourceNode>().SetupNode(this);
        }
    }

    
}
