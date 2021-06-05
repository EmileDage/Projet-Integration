using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Regen : Stats
{
    private float currentValue;
    public Stats regenValue = null;
    public bool canRegen = true;

    public float GetCurrentValue()
    {
        return currentValue;
    }
    public void DecreaseCurrentValue(float Amount)
    {
        currentValue = Mathf.Clamp(currentValue -= Amount, -1, Value());
    }
    public void IncreaseCurrentValue(float Amount)
    {
        currentValue = Mathf.Clamp(currentValue += Amount, -1, Value());
    }
    public void InitializeRecovery()
    {
        currentValue = Value();
    }
    public void StartRecovery()
    {
        if (canRegen)
            Recover();
    }
    private float Recover()
    {
        if (currentValue >= Value()) return currentValue;
        currentValue += regenValue.Value() * Time.deltaTime;
        return Mathf.Clamp(currentValue, 0, Value());
    }
    public bool CurrentIsEmpty()
    {
        return currentValue <= 0;
    }
}
