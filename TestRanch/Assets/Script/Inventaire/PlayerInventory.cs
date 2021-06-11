using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : AbstractInventoryUI
{
    [SerializeField] private Item testItem;
    public int selectedIndex = 0;
    private Slot[] slots;
    private Image slotImg;
    GameManager GM;
    Player joueur;
    



    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        GM = GameManager.gmInstance;
        joueur = GM.Joueur;
        joueur.BarreInventaire = this;

        for(int i = 0; i<slots.Length;i++){

            slotImg = slots[i].GetComponent<Image>();
            SetSlotsParent(slots[i]);
            slots[i].DeSelect();

            if (i < joueur.InventaireTaille)
            {
                slotImg.sprite = slots[i].Box;
                slots[i].UpdateSlot();
            }
            else 
            { 
                slotImg.sprite = slots[i].LockedBox;
                slots[i].UpdateSlot();
            }
        }

        SelectItem(0);
    }
    #region removed
    /* private void OnItemRemoved(Inventaire source, ItemStack item)
     {
         foreach (var stack in slots)
         {
             if (stack.ItemStack.Equals(item))
             {
                 stack.UpdateSlot();
                 break;
             }
         }
     }

     public void OnItemsChanged(Inventaire source)
     {
         UpdateAllSlots();
     }
    */
    #endregion




    public void UpdateAllSlots()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateSlot();
        }
    }

    //pour parcourir avec la roue
    public void ScrollItembar(int difference)
    {
        slots[selectedIndex].DeSelect();
        selectedIndex += difference;
        selectedIndex = selectedIndex % joueur.InventaireTaille;
        slots[selectedIndex].DeSelect();      
        slots[selectedIndex].Select();
        joueur.Selected = slots[selectedIndex];
    }

    //pour parcourir avec 12345...
    public void SelectItem(int index)
    {
        slots[selectedIndex].DeSelect();
        slots[index].Select();
        selectedIndex = index;
        joueur.Selected = slots[index];
    }

    public void MergeOnExisting(ItemStack stack)
    {
 //       Debug.Log("TryMergeOnExisting");   
        foreach (Slot slot in slots)
        {
            slot.TryMerge(stack);
        }
        Debug.Log(stack.Qte);
    }


    public int GetFirstEmptySlotIndex()
    {
        for (int i = 0; i < joueur.InventaireTaille; i++)
        {
            if(slots[i].ItemStack.Item.ID == 0)
            {
                return i;
            }
        }
        return -1;
    }

    public Slot GetFirstEmptySlot()
    {
        int ret = GetFirstEmptySlotIndex();
        if (ret != -1)
        {
            return slots[ret];
        }
        else
        {
            return null;
        }
    }

    public void AddOnFirstEmptySlot(ItemStack stack)
    {
        int work = GetFirstEmptySlotIndex();
        if (work>-1 && work < joueur.InventaireTaille)
        {
            slots[work].AddItemInSlot(stack);           
        }
        
    }

    public bool TryAddOnEmptySlot(ItemStack stack)
    {
        int work = GetFirstEmptySlotIndex();
        if (work > -1 && work < joueur.InventaireTaille)
        {        
            slots[work].AddItemInSlot(stack);
            return true;
        }
        return false;
    }

    public int QuickAddItem(ItemStack stack)
    {
        MergeOnExisting(stack);
        if(stack.Qte> 0) { 
            AddOnFirstEmptySlot(stack);
        }
        Debug.Log(stack.Qte);
        return stack.Qte;
    }

    public bool TryMergeOnExisting(ItemStack stack)
    {
        bool ret;
        foreach (Slot slot in slots)
        {
          ret =  slot.TryMerge(stack);
            if (ret)
            {

                return ret;
            }
        }
        Debug.Log(stack.Qte);
        return false;
    }
   
    public bool TryPayWithItemStack(ItemStack price)
    {
        foreach (Slot slot in slots)
        {
            if (slot.PayInItem(price))
                return true;
        }
        return false;
    }

    public bool TryPayWithMultipleItems(List<ItemStack> list)
    {
        foreach (ItemStack item in list)
        {
            if (!TryPayWithItemStack(item))
            {
                return false;
            }
        }
        return true;
    }
    //de this vers other
    public override void QuickSendStack(ItemStack stack, DragItem drag)
    {
        Coffre  c=  GM.Joueur.OpenChest;
        if(c != null) { 
            c.GetCoffreUI().TryMergeOnExisting(stack);
            if (c.GetCoffreUI().GetFirstEmptySlot() != null) 
            {
                Debug.Log("empty slot dispo");
                c.GetCoffreUI().GetFirstEmptySlot().SwapItems(drag);
                //inv.GetFirstEmptySlot().SwapItems(drag);
            }
        }
        else
        {

        }

    }

    //ça marche de l'inventaire vers le player mais pas du player vers l'inventaire?

    #region test
    private void Update()
    {
        if (Input.GetButtonDown("DebugAddRessource"))
        {
            QuickAddItem(new ItemStack(this.testItem, 6));         
        }
    }
    #endregion

}
