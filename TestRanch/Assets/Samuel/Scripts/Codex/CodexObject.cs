using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodexObject : MonoBehaviour
{
    [Header("Codex ScriptableObjects")]
    [SerializeField] private CodexScriptable codexEntry = null;
  //  [SerializeField] private CodexScriptable emptyCodex = null;

    private bool isDiscover = false;

    [Header("Codex UI")]
    [SerializeField] private List<Text> txtNames = null;
    [SerializeField] private Text txtDescription = null;
    [SerializeField] private Image icon = null;

    public void Discover()
    {

        if (!isDiscover)
        {
            foreach (Upgrade upgrade in codexEntry.GetListOfUpgrade())
            {
                UpgradeManager.upgradeInstance.DiscoverUpgrade(upgrade);
            }
                isDiscover = true;
                UpdateVisual(codexEntry);
        }
        else
            return;
           
    }
    public bool IsDiscover()
    {
        return isDiscover;
    }
    public CodexScriptable GetCodex()
    {
        return codexEntry;
    }
    public void UpdateVisual(CodexScriptable codex)
    {
        if(isDiscover)
        {
            foreach (Text text in txtNames)
            {
                text.text = codex.GetName();
            }
            txtDescription.text = codex.GetDescription();
            icon.sprite = codex.GetIcon();
        }
    }

}
