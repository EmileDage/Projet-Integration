using System.Collections;
using System.Collections.Generic;

public class NatureMineSpawner : AbstractSpawner, IMinable
{//on tape le tronc a la place des nodes avec ça


    protected override void Start()
    {
        base.Start();
       // gm = GameManager.gmInstance;
    }
    public void Mine()
    {
        foreach (var item  in produits) {
            if (item.gameObject.activeSelf) {
                //item.CollectNode();
                
              //  item.GetComponent<>().Collect(gm.Joueur);
                return;
            }
        }
    }
}

