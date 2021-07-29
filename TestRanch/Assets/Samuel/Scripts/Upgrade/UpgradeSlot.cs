using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [Header("Upgrade State")]
    [SerializeField] private Upgrade upgrade = null;
    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private bool isActivated = false;


    [Header("Upgrade UI")]
    [SerializeField] private Text txtName = null;
    [SerializeField] private Text txtDescription = null;
    [SerializeField] private Text txtCost = null;
    [SerializeField] private Text txtPrerequisites = null;
    [SerializeField] private Image icon = null;

    public bool IsUnlockedSave { get => isUnlocked; set => isUnlocked = value; }
    public bool IsActivatedSave { get => isActivated; set => isActivated = value; }

    private void Start()
    {


        txtName.text = upgrade.GetName();
        txtDescription.text = upgrade.GetDescription();
        txtCost.text = upgrade.GetCost().ToString() + " $ ";
        txtPrerequisites.text = PrerequisiteToString();
        icon.sprite = upgrade.GetIcon();
    }
    public void Unlock()
    {
        isUnlocked = true;
        gameObject.SetActive(true);
    }
    public bool IsUnlocked()
    {
        return isUnlocked;
    }
    public bool TryToUpgrade()
    {
        if (!CheckChronoCoin(upgrade.GetCost()))
        {
            GetComponent<Image>().color = Color.red;
            return false;
        }

        if (!PrerequisiteComplete())
        {
            GetComponent<Image>().color = Color.red;
            return false;
        }

        if (isActivated)
            return false;

        return true;
    }
    public void Activate()
    {
        isActivated = true;
        gameObject.SetActive(true);
        GetComponent<Image>().color = Color.green;
        GameManager.gmInstance.ModifyChronoCoin(upgrade.GetCost(), true);

    }
    public bool IsActivated()
    {
        return isActivated;
    }

    public Upgrade GetUpgrade()
    {
        return upgrade;
    }

    private bool CheckChronoCoin(int value)
    {
        if ((GameManager.gmInstance.GetChronoCoin() - value) >= 0)
            return true;
        else return false;
    }

    private string PrerequisiteToString()
    {
        string text = "Prerequisite : ";

        foreach (Upgrade upgrade in upgrade.GetPrerequisiteList())
        {
            text += upgrade.GetName() + ", ";
        }
        return text;
    }
    private bool PrerequisiteComplete()
    {
        if (upgrade.GetPrerequisiteList().Count != 0)
        {
            foreach (Upgrade upgrade in upgrade.GetPrerequisiteList())
            {
                foreach (UpgradeSlot upgradeSlot in UpgradeManager.upgradeInstance.GetUpgradeList())
                {
                    if(upgrade == upgradeSlot.GetUpgrade())
                    {
                        if (!upgradeSlot.IsActivated())
                            return false;
                    }
                }
            }
            return true;
        }
        return true;
    }


}
