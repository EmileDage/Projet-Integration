using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceNode : MonoBehaviour
{
    private void Start()
    {
        Debug.LogError("Mauvais Node, updater avec le SimpleNode");
    }
    //desuet
    /* private AbstractSpawner mother;
     private int place;
     private int cd;//cooldown
     private int respawnTime;
     private bool spawned = true;
     private bool isDead = false;



     private MyTimeManager time;

     public bool Spawned { get => spawned;}
     public bool IsDead { get => isDead; set => isDead = value; }


     private void Start()
     {
         time = MyTimeManager.timeInstance;
         time.GHourPassed += OnGHourPassed;
     }

     public void OnChronoUpgrade(int newRespawnTime) {
         respawnTime = newRespawnTime;
     }



     private void OnGHourPassed(object source)
     {
         if (!isDead) {            
             cd--;
             if (mother.GetComponent<SpawnerAgriculture>() != null)
             {
                 if (cd <= 0  && mother.GetComponent<SpawnerAgriculture>().GrownYet)
                 {
                     this.gameObject.SetActive(true);
                     SetSpawnedTrue();
                 }
             }
             else {
                 if (cd <= 0)
                 {
                     this.gameObject.SetActive(true);
                     SetSpawnedTrue();
                 }
             }


         }
     }

     public void SetSpawnedTrue()
     {
         if(!isDead)
             spawned = true;
     }



     public bool GetSpawned() {
         return spawned;
     }

     public void DeSpawnNode()
     {
         this.gameObject.SetActive(false);
         spawned = false;
         cd = respawnTime; 
     }

     public void KillNode()
     {
         this.DeSpawnNode();
         isDead = true;
     }

     public void SetupNode(AbstractSpawner motherRef)
     {

         mother = motherRef;
         respawnTime = motherRef.TimeToRespawnRef;
         cd = respawnTime;
         this.SetSpawnedTrue();
         this.gameObject.SetActive(true);
         GetComponent<Rigidbody>().isKinematic = true;
     }

     public void Collect(Player joueur)
     {
         //int x = Mathf.FloorToInt(ParentSpawner.Yield * joueur.Selected.ItemStack.GetYieldModifier());
         //joueur.BarreInventaire.QuickAddItem(new ItemStack(mother.SpawnedMat(), 1));
         this.DeSpawnNode();
     }*/

}
