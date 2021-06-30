
using UnityEngine;

[RequireComponent(typeof(SimpleNode))]
public class MinableNodeModule : MonoBehaviour, IMinable
{
    SimpleNode node;

    public void Mine(Player joueur)
    {
        node.CollectNode(joueur);
    }

    private void Start()
    {
        node = GetComponent<SimpleNode>();
    }


}

