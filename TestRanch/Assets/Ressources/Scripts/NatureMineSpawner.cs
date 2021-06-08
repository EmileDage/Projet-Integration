using System.Collections;
using System.Collections.Generic;

public class NatureMineSpawner : AbstractSpawner, IMinable
{//on tape le tronc a la place des nodes avec ça
    GameManager gm;

    protected override void Start()
    {
        base.Start();
        gm = GameManager.gmInstance;
    }
    public void Mine()
    {
        foreach (var item  in produits) {
            if (item.gameObject.activeSelf) {
                item.GetComponent<RessourceNode>().Collect(gm.Joueur);
                break;
            }
        }
    }
}

