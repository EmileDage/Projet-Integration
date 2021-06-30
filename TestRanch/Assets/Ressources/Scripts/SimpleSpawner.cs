using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleSpawner : MonoBehaviour
{
    
    protected List<SimpleNode> produits;
    [SerializeField] protected Materiaux spawnedMateriaux;
    [SerializeField] [Range(0, 23)] protected int timeToRespawn;
    [SerializeField] [Range(0, 23)] protected int disponibleStart; //si on veut que l'objet soit collectable seulement pendant une période de la journee
    [SerializeField] [Range(0, 23)] protected int disponibleEnd;
    


    protected MyTimeManager time;
    
    public int TimeToRespawnRef{ get => timeToRespawn; set => timeToRespawn = value; }
    public Materiaux Mat { get => spawnedMateriaux; }
    public List<SimpleNode> Produits { get => produits;}

    protected virtual void Start() 
    {
        produits = new List<SimpleNode>();
        time = MyTimeManager.timeInstance;
        time.GHourPassed += OnGHourPassed;

        bool active = false;
        if(disponibleStart < time.Hour)
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

        if (disponibleStart == time.Hour)
        {
            MakeDisponible();
        }
        else if (disponibleEnd == time.Hour)
        {
            MakeIndisponible();          
        }
    }

    protected virtual void MakeDisponible() {
        
        foreach (SimpleNode produit in produits)
        {
            if (produit.WorkCD < 0) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
            { //note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                produit.RespawnNode();
            }
        }
    }

    protected virtual void MakeIndisponible()
    {
        foreach (SimpleNode produit in produits)
        {
            produit.gameObject.SetActive(false);
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

    
    public virtual  void KillAllNode()
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
}
