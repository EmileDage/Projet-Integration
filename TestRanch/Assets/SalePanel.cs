using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalePanel : AbstractInventoryUI
{
    [SerializeField] private Text value;
    private int saleValue;

    private Slot[] slots;
    UIManager UI;
    GameManager gm;
    private void Start()
    {
        slots = this.GetComponentsInChildren<Slot>();
        UI = UIManager.Instance;
        gm = GameManager.gmInstance;
        value.text = "0$";
        foreach (Slot slot in slots)
        {
            SetSlotsParent(slot);
            slot.UpdateSlot(false);
        }
    }

    public void SellAll()
    {
        gm.ModifyChronoCoin(saleValue);
        foreach (Slot slot in slots)
        {
            slot.RemoveItem();
        }
        UpdatePanel();
        UI.ExitPanel(this.gameObject);
    }

    public override void UpdatePanel()
    {
        saleValue = 0;
        foreach (Slot slot in slots)
        {
            saleValue += slot.ItemStack.GetValue();
        }
        value.text = saleValue.ToString()+ "$";
    }
}
