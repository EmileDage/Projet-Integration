using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNature : AbstractSpawner
{
    
    protected override void Start()
    {
        base.Start();
    }



    public override void OnGHourPassed(object source)
    {
        if(disponibleStart == time.Hour)
        {
            foreach (GameObject produit in produits)
            {
                if (produit.GetComponent<RessourceNode>().GetSpawned()) //on ne veut pas activer le node si il n'a pas eu le temps de respawn
                    //note la ressourceNode.GetSpawned ne va jamais retourne vrai si le node est mort
                { 
                    produit.SetActive(true);
                }
            }
        }
        else if(disponibleEnd == time.Hour)
        {
            foreach (GameObject produit in produits)
            {
                produit.SetActive(false);
            }
        }
    }

    public override void SpawnProduce()
    {
        for (int a = 0; a < produit_spawn.Length; a++)
        {
            if (produits[a] == null)
            {
                produits[a].GetComponent<RessourceNode>().SetSpawnedTrue();
            }
        }
    }

  

    //Si une maladie est fatale
    public override void DestroyAll()
    {
        for (int a = 0; a < produits.Length; a++)
        {
            produits[a].GetComponent<RessourceNode>().KillNode();
        }
        Debug.Log("All products on this spawner are destroyed");
    }
}
