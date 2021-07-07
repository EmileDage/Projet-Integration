using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCapturedStation : MonoBehaviour
{
    public List<CreatureBehavior> creature;

    public void AddToList(CreatureBehavior pokeball)
    {
        creature.Add(pokeball);
        pokeball.onlyOnce = true;
    }
}
