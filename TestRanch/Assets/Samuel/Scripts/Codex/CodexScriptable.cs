using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Codex", menuName = "ScriptableCodex/Codex")]

public class CodexScriptable : ScriptableObject
{
    [SerializeField] private string codexName = "None";
    [TextArea(3, 10)]
    [SerializeField] private string description = "Default Description";
    [SerializeField] private Sprite icon = null;
    [SerializeField] private List<Upgrade> upgradeUnlocked = null;


    public string GetName()
    {
        return codexName;
    }
    public string GetDescription()
    {
        return description;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public List<Upgrade> GetListOfUpgrade()
    {
        return upgradeUnlocked;
    }
}
