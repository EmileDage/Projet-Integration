using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleSpawner : MonoBehaviour
{
    
    protected List<SimpleNode> produits;
    [SerializeField] protected Materiaux spawnedMateriaux;
    [SerializeField] [Range(0, 23)] protected int timeToRespawn;
    [SerializeField] protected bool AlwaysAvailable = false;
    [SerializeField] [Range(0, 23)] protected int disponibleStart; //si on veut que l'objet soit collectable seulement pendant une période de la journee
    [SerializeField] [Range(0, 23)] protected int disponibleEnd;
    


    protected MyTimeManager time;
    
    public int TimeToRespawnRef{ get => timeToRespawn; set => timeToRespawn = value; }
    public Materiaux Mat { get => spawnedMateriaux; }
    public List<SimpleNode> Produits { get => produits;}

    protected virtual void Start() 
    {
       // Debug.Log("Start" + this);
        produits = new List<SimpleNode>();
        time = MyTimeManager.timeInstance;
        time.GHourPassed += OnGHourPassed;

        bool active = false;
        if(disponibleStart < time.Hour || AlwaysAvailable)
        {
            active = true;
        }

        foreach (SimpleNode item in this.GetComponentsInChildren<SimpleNode>())
        {
           
            produits.Add(item);
            item.Cooldown(timeToRespawn);
            item.MatNode = spawnedMateriaux;
            item.gameObject.SetActive(active);
        }

    }


    public virtual void OnGHourPassed(object source) {
        if (!AlwaysAvailable) { 
            if (disponibleStart == time.Hour)
            {
                MakeDisponible();
            }
            else if (disponibleEnd == time.Hour)
            {
                MakeIndisponible();          
            }
        }
    }

    protected virtual void MakeDisponible() {
        
        foreach (SimpleNode produit in produits)
        {
           // Debug.Log("make dispo");
          //  Debug.Log(produit.WorkCD);
            if (produit.WorkCD <= 0) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
            { 
                produit.NodeDisponibleSpawn();
            }
        }
    }

    protected virtual void MakeIndisponible()
    {
       // Debug.Log(produits);
        foreach (SimpleNode produit in produits)
        {
            produit.NodeIndisponible();
        }
    }

    /*public virtual void SpawnProduce() {
        for (int a = 0; a < produit_spawn.Length; a++)
        {
            if (produits[a] == null)
            {
                produits[a].GetComponent<RessourceNode>().SetSpawnedTrue();
            }
        }
    }*/

    
    public virtual void KillAllNode()
    {
        for (int a = 0; a < produits.Count; a++)
        {
            produits[a].KillNode();
        }
        
    }
   
    public virtual void OnChronoUpgrade()
    {
        timeToRespawn /= 2;//on peut le changer plus tard si cest pas balancer ou whatever

        foreach (SimpleNode node in produits)
        {
            node.Cooldown(timeToRespawn);
        }

    }


    public virtual void SpawnSpawner(Materiaux toSpawn)
    {
        Debug.LogWarning("fonction desuette");
    }


    private void OnDestroy()
    {
        if (time != null)
        {
            time.GHourPassed -= OnGHourPassed;
        }
    }

    private void OnDisable()
    {
        if (time != null)
        {
            time.GHourPassed -= OnGHourPassed;
        }
    }
}
