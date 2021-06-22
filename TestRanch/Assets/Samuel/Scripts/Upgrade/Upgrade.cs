using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableUpgrade/Upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private string upgradeName = "None";
    [TextArea(3,10)]
    [SerializeField] private string description = "Default Description";
    [SerializeField] private int cost = 0;
    [SerializeField] private Sprite icon = null;
    [SerializeField] private List<Upgrade> prerequisite = null;

   
    public string GetName()
    {
        return upgradeName;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetCost()
    {
        return cost;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public List<Upgrade> GetPrerequisiteList()
    {
        return prerequisite;
    }


}
