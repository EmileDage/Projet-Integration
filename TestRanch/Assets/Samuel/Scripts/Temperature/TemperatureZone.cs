using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureZone : MonoBehaviour
{
    [SerializeField] private int zoneTemperature = 0;
    private int playerTemperature = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerTemperature = other.GetComponent<TemperatureModule>().GetCurrentTemperatureLevel();
            other.GetComponent<TemperatureModule>().SetTemperatureLevel(zoneTemperature);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TemperatureModule>().SetTemperatureLevel(playerTemperature);
        }
    }

}
