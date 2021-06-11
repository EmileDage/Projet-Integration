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

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UI.ExitPanel(this.gameObject);
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
        gameObject.SetActive(false);
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
            saleValue += slot.ItemStack.GetValue();
        }
        value.text = saleValue.ToString()+ "$";
    }
}
