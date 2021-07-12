
using UnityEngine;

[RequireComponent(typeof(SimpleNode))]
[RequireComponent(typeof(AudioSource))]
public class MinableNodeModule : MonoBehaviour, IMinable
{
    SimpleNode node;
    
    [SerializeField] AudioClip mineClip;

    public void Mine(Player joueur)
    {
        node.CollectNode(joueur);
        this.GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        node = GetComponent<SimpleNode>();
        this.GetComponent<AudioSource>().clip = mineClip;
    }


}

