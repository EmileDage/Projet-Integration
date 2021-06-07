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

    protected MyTimeManager time;
    public GameObject Produit_reference { get => produit_reference; set => produit_reference = value; }



    protected virtual void Start() {//a mettre dans start set up les trucs de base
        if(produit_reference != null) { 
            produits = new GameObject[produit_spawn.Length];
            time = MyTimeManager.timeInstance;
            time.GHourPassed += OnGHourPassed;

            for (int i = 0; i < produit_spawn.Length; i++)
            {
                produits[i] = Instantiate(Produit_reference, produit_spawn[i]);
                produits[i].tag = "produit";
                produits[i].AddComponent<RessourceNode>();
                produits[i].GetComponent<RessourceNode>().SetupNode(this, i, timeToRespawn);
            }
        }
    }

    public abstract void OnGHourPassed(object source);


    public abstract void SpawnProduce();

    //exemple arbre est malade dont tous ces fruits pourrissent/roche rot idk
    public abstract void DestroyAll();

    public void OnChronoUpgrade()
    {
        timeToRespawn = timeToRespawn / 2;
    }

    //change destroy 1 pour destroy1atrandom, destroyone deplace vers le ressourcenode
    public void DestroyOneAtRandom()
    {
        produits[UnityEngine.Random.Range(0, produits.Length)].GetComponent<RessourceNode>().KillNode();//yark c lette
    }

    public virtual void SpawnSpawner(Materiaux toSpawn)
    {
        produit_reference = toSpawn.ItemWorldObject;
        produits = new GameObject[produit_spawn.Length];
        time = MyTimeManager.timeInstance;
        time.GHourPassed += OnGHourPassed;

        for (int i = 0; i < produit_spawn.Length; i++)
        {
            produits[i] = Instantiate(Produit_reference, produit_spawn[i]);
            produits[i].tag = "produit";
            produits[i].AddComponent<RessourceNode>();
            produits[i].GetComponent<RessourceNode>().SetupNode(this, i, timeToRespawn);
        }
    }

    
}
