using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [Header("Upgrade State")]
    [SerializeField] private Upgrade upgrade = null;
    [SerializeField] private bool isUnlocked = false;

    [Header("Upgrade UI")]
    [SerializeField] private Text txtName = null;
    [SerializeField] private Text txtDescription = null;
    [SerializeField] private Text txtCost = null;
    [SerializeField] private Image icon = null;

    private void Start()
    {
        txtName.text = upgrade.GetName();
        txtDescription.text = upgrade.GetDescription();
        txtCost.text = upgrade.GetCost().ToString() + " $ ";
        icon.sprite = upgrade.GetIcon();
        UpdateVisuel();
    }

    private void Update()
    {
        if (upgrade.IsActivated())
            GetComponent<Image>().color = Color.red;
    }
    public bool IsUnlocked()
    {
        return isUnlocked;
    }
    public void Unlock()
    {
        if (PrerequisiteComplete())
        {
            isUnlocked = true;
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Prerequisite not complete");
            return;
        }
    }
    public void UpdateVisuel()
    {
        if (isUnlocked)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
    bool PrerequisiteComplete()
    {
        if (upgrade.GetPrerequisiteList().Count != 0)
        {
            foreach (Upgrade upgrade in upgrade.GetPrerequisiteList())
            {
                if (!upgrade.IsActivated())
                {
                    Debug.Log("Upgrade Missing");
                    return false;
                }
            }
            return true;
        }
        return true;
    }
}
