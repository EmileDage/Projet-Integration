using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{

    [SerializeField] protected float baseValue = 0;

    private List<float> modifiers = new List<float>();

    public float Value()
    {
        float finalvalue = baseValue;
        modifiers.ForEach(x => finalvalue += x);
        return finalvalue;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

    public bool RollADice()
    {
        int randomValue = Random.Range(0, 101);
        return baseValue > randomValue;
    }
}
