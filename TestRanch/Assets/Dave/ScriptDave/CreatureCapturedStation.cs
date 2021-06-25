using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCapturedStation : MonoBehaviour
{
    public List<CreatureBehavior> creature;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(CreatureBehavior pokeball)
    {
        creature.Add(pokeball);
        pokeball.onlyOnce = true;
    }
}
