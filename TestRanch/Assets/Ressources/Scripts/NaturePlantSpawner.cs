using UnityEngine;

public class NaturePlantSpawner : SimpleSpawner, IFarmable
{//on tape le tronc a la place des nodes avec ça
   
    GameManager gm;
  

    protected override void Start()
    {
        base.Start();
        gm = GameManager.gmInstance;
    }


    public void FarmIt()
    {
        Debug.LogError("Not implemented");
        //donne un materiaux type plante
    }

}

