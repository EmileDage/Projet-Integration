using UnityEngine;

public class NaturePlantSpawner : AbstractSpawner, IFarmable
{//on tape le tronc a la place des nodes avec ça
   
    GameManager gm;
  

    protected override void Start()
    {
        base.Start();
        gm = GameManager.gmInstance;
    }

    private void OnDestroy()
    {
        if (time != null) 
        { 
            time.GHourPassed -= OnGHourPassed;
        }
    }

    public void FarmIt()
    {
        foreach (var item in produits)
        {
            if (item.gameObject.activeSelf)
            {
                Debug.LogError("Not Implemented");
               // item.GetComponent<RessourceNode>().Collect(gm.Joueur);
                return;
            }
        }

    }

}

