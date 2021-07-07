using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureModule : MonoBehaviour
{
    [SerializeField] private Regen temperature = null;
    [SerializeField] private float consumptionValue = 1;
    [SerializeField] private Image temperatureBar = null;
    [SerializeField] private float freezeDamage = 1;
    private bool coldResistanceUpgrade = false;

    private float delay = 0;
    private int temperatureLevel = 0;

    private void Start()
    {
        temperature.InitializeRecovery();
    }
    private void Update()
    {
        temperature.StartRecovery();
        temperatureBar.fillAmount = temperature.GetCurrentValue() / temperature.Value();

        if (delay > 0)
            delay = Mathf.Clamp(delay -= 1 * Time.deltaTime, 0, 1000);

        if (temperatureLevel == 0)
            return;

        else if (temperatureLevel == -1)
            DecreaseTemperatureValue();

    }

    public void UpgradeColdResistance()
    {
        coldResistanceUpgrade = true;
    }
    public void SetTemperatureLevel(int level)
    {
        if (level == 0)
            temperature.canRegen = true;
        else if (level == -1)
            temperature.canRegen = false;

        temperatureLevel = level;
    }
    public int GetCurrentTemperatureLevel()
    {
        return temperatureLevel;
    }
    public void DecreaseTemperatureValue()
    {
        if (delay <= 0 && temperature.Value() > 0 && !coldResistanceUpgrade)
        {
            temperature.DecreaseCurrentValue(consumptionValue);
            if(temperature.GetCurrentValue() <= 0)
                GameManager.gmInstance.Joueur.GetComponent<HealthModule>().DecreaseHealth(freezeDamage);

            delay = 0.1f;
        }
    }
}
