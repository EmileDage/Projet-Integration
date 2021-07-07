using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeSlot> upgradesList = new List<UpgradeSlot>();
    private GameObject player = null;

    public static UpgradeManager upgradeInstance;

    void Awake()
    {

        if (upgradeInstance != null && upgradeInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            upgradeInstance = this;
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void DiscoverUpgrade(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if(upgrade == upgradeSlot.GetUpgrade())
            {
                upgradeSlot.Unlock();
                break;
            }
        }
    }


    #region SpeedUpgrade
    public void UpdradeSpeedI(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<MovementModule>().ModifySpeed(20);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeSpeedII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<MovementModule>().ModifySpeed(20);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeSpeedIII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<MovementModule>().ModifySpeed(20);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    #endregion
    #region StaminaUpgrade
    public void UpdradeStaminaI(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<StaminaModule>().ModifyStamina(50);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeStaminaII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<StaminaModule>().ModifyStamina(100);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeStaminaIII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<StaminaModule>().ModifyStamina(150);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    #endregion
    #region HealthUpgrade
    public void UpdradeHealthI(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<StaminaModule>().ModifyStamina(50);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeHealthII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<StaminaModule>().ModifyStamina(100);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeHealthIII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<StaminaModule>().ModifyStamina(150);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    #endregion
    #region InventoryUpgrade
    public void UpdradeInventoryI(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<Player>().IncreaseInventorySize(5);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeInventoryII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<Player>().IncreaseInventorySize(7);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeInventoryIII(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.GetComponent<Player>().IncreaseInventorySize(9);
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    #endregion
    public void UpgradeColdResistance(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    GameManager.gmInstance.Joueur.GetComponent<TemperatureModule>().UpgradeColdResistance();
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeWarpStone(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    WaypointsManager.waypointManagerInstance.UnlockWarpStone();
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
    public void UpdradeJetPack(Upgrade upgrade)
    {
        foreach (UpgradeSlot upgradeSlot in upgradesList)
        {
            if (upgradeSlot.GetUpgrade() == upgrade)
            {
                if (upgradeSlot.TryToUpgrade())
                {
                    player.AddComponent<JetPackInput>();
                    player.AddComponent<JetPackModule>();
                    upgradeSlot.Activate();
                }
                break;
            }
        }
    }
}
