using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleNode))]
public class InteractibleNodeModule : MonoBehaviour, IInteractible
{
    SimpleNode node;
    private void Start()
    {
        node = GetComponent<SimpleNode>();
    }

    public void Interact(Player joueur)
    {
        
        node.CollectNode(joueur);        
    }
}

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

