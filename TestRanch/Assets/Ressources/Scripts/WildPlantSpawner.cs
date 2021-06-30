using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WildPlantSpawner : SimpleSpawner, IFarmable
{
    [SerializeField] private Materiaux plante;
    [SerializeField] private int hp = 1;
    public void FarmIt()
    {
        GameObject loot = Instantiate(plante.ItemWorldObject);
        loot.GetComponent<WorldObjectMateriaux>().Qte = 1;
        loot.GetComponent<WorldObjectMateriaux>().Interact(GameManager.gmInstance.Joueur);
        hp--;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}