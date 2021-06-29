using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeState
{
    public virtual void SpawnNode(GameObject obj) { }

}

public class NodeAlive : NodeState
{

    public override void SpawnNode(GameObject obj)
    {
        obj.SetActive(true);
    }
}

public class NodeDead : NodeState
{
  public override void SpawnNode(GameObject obj)
    {
        Debug.Log("node is dead");
    }
}

public class SimpleNode : MonoBehaviour
{
    private Materiaux matNode;
    private NodeState nodeState;
    int cooldown;
    int workCD;

    private int yield = 1;

    public void Cooldown(int cd)
    {
        cooldown = cd;
        if (workCD > cd)
            workCD = cd;
        
    }
    public int WorkCD { get => workCD;}
    public Materiaux MatNode { get => matNode; set => matNode = value; }
    public int Yield { get => yield; set => yield = value; }

    // Start is called before the first frame update
    void Start()
    {
        MyTimeManager.timeInstance.GHourPassed += OnGHourPassed;
        nodeState = new NodeAlive();
        workCD = cooldown;
    }

    private void OnGHourPassed(object source)
    {
        if (workCD > 0) {
            workCD--; 
        }else if(workCD == 0)
        {
            nodeState.SpawnNode(this.gameObject);
        }
    }

    public void RespawnNode()
    {
        nodeState.SpawnNode(this.gameObject);
    }

    public void KillNode()
    {
        nodeState = new NodeDead();
        this.gameObject.SetActive(false);
    }

    public void ReviveNode()
    {
        nodeState = new NodeAlive();
    }

    public void CollectNode(Player joueur)
    {
        this.gameObject.SetActive(false);
        GameObject loot = Instantiate(MatNode.ItemWorldObject);
        loot.GetComponent<WorldObjectMateriaux>().Qte += yield;
        Debug.Log(loot + " loot");
        loot.GetComponent<WorldObjectMateriaux>().Interact(joueur);
        workCD = cooldown;
    }

    private void OnDestroy()
    {
        if (MyTimeManager.timeInstance != null)
        {
            MyTimeManager.timeInstance.GHourPassed -= OnGHourPassed;//unsubscribe a l'event
        }
    }

}
