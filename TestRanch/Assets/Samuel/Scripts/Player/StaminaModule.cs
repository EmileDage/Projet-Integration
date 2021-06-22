using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaModule : MonoBehaviour
{
    [SerializeField] private Regen Stamina = null;
    [SerializeField] private float consumptionValue = 1;
    [SerializeField] private float exhaustDuration = 7;

    private float exhaustTimer = 0;
    private float delay = 0;


    void Start()
    {
        Stamina.InitializeRecovery();
    }

    void Update()
    {
        Stamina.StartRecovery();

        if(delay > 0)
         delay = Mathf.Clamp(delay -= 1 * Time.deltaTime,0,1000);

        if (exhaustTimer > 0)
            exhaustTimer = Mathf.Clamp(exhaustTimer -= 1 * Time.deltaTime, 0, 1000);

    }

    public void DecreaseStamina()
    {
        if(delay <= 0 && Stamina.Value() > 0 && exhaustTimer <= 0)
        {
            Stamina.DecreaseCurrentValue(consumptionValue);
            delay = 0.1f;
        }
    }
    public void ActivateStaminaUse()
    {
        if(Stamina.Value() >= 0 && exhaustTimer <= 0)
        {
            Stamina.canRegen = false;
        }
    }

    public void ModifyStamina(float value, bool RemoveValue = false)
    {
        if (!RemoveValue)
            Stamina.AddModifier(value);
        else
            Stamina.RemoveModifier(value);

    }

    public void DesactivateStaminaUse()
    {
        Stamina.canRegen = true;
    }
    public Regen GetStamina()
    {
        return Stamina;
    }
    public bool IsExhaust()
    {
        return exhaustTimer != 0;
    }
    public void ActivateExhaust()
    {
        exhaustTimer = exhaustDuration;
    }
}
