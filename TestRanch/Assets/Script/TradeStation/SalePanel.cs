using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalePanel : AbstractInventoryUI
{
    [SerializeField] private Text value;
    [SerializeField] private float sellPriceModif = 0.5f;
    private int saleValue;

    private Slot[] slots;
    UIManager UI;
    GameManager gm;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CancelSell();
            gm.Joueur.OpenedNonChestInventory = null;
        }
    }

    private void Start()
    {
        slots = this.GetComponentsInChildren<Slot>();
        UI = UIManager.Instance;
        gm = GameManager.gmInstance;
        value.text = "0$";
        foreach (Slot slot in slots)
        {
            SetSlotsParent(slot);
            slot.UpdateSlotWithoutPanel();
        }
       
    }

    public void SellAll()
    {
        gm.ModifyChronoCoin(saleValue);
        foreach (Slot slot in slots)
        {
            slot.RemoveItem();
            slot.UpdateSlotWithoutPanel();
        }
        UpdatePanel();
        UI.ExitPanel(this.gameObject);
        gm.Joueur.OpenedNonChestInventory = null;
    }

    public override void UpdatePanel()
    {
        saleValue = 0;
        foreach (Slot slot in slots)
        {
            float t = slot.ItemStack.GetValue() * sellPriceModif;
            saleValue += Mathf.FloorToInt(t);
        }
        value.text = saleValue.ToString()+ "$";
    }

    public void CancelSell()
    {
        foreach (Slot slot in slots)
        {
            if(slot.ItemStack.Qte > 0)
            {
                gm.GetPlayerInventory().QuickAddItem(slot.ItemStack);
                slot.RemoveItem();
                slot.UpdateSlotWithoutPanel();
            }
        }
        UI.ExitPanel(this.gameObject);  
    }
}
