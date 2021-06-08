using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffreUI : AbstractInventoryUI
{
    //serialize pour tester

    private List<Slot> slots;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject parent;


    GameManager GM;
    private Coffre chestInUse;

    private int taille =9;
    public int Taille { get => taille; set => taille = value; }
    public Coffre ChestInUse { get => chestInUse; set => chestInUse = value; }

    private void Start()
    {
        slots = new List<Slot>();
        parent.SetActive(false);
        GM = GameManager.gmInstance;
    }

    public void SetUp(int size, List<ItemStack> content, Coffre chest)
    {
        chestInUse = chest;
        for (int i=0; i<size; i++)
        {
            SetUpOneCase(i);
            slots[i].ItemStack = content[i];
        }
        UpdateAllSlots();
    }

    public void OpenChest()
    {
        parent.SetActive(true);
    }

    public void SetUpOneCase(int i)
    {
        slots.Add(Instantiate(slotPrefab).GetComponent<Slot>());
        slots[i].transform.SetParent(this.transform, false);
        slots[i].DragItem.Canvas = canvas;
        slots[i].DeSelect();
        SetSlotsParent(slots[i]);
    }

    public void CloseChest()
    {
        GM.Joueur.CloseChest();//desassigne
        UpdateAllSlots();
        foreach (Slot slot in slots)
        {            
            Destroy(slot.gameObject);         
        }
        slots.RemoveRange(0, slots.Count);
    }

    public void UpdateAllSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateSlot();
            chestInUse.Contenu[i] = slots[i].ItemStack;
        }
    }

    public void TryMergeOnExisting(ItemStack stack)
    {
        Debug.Log("TryMergeOnExisting, coffreUI");

        foreach (Slot slot in slots)
        {
            slot.TryMerge(stack);
        }
        
    }


    public int GetFirstEmptySlotIndex()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].ItemStack.Item.ID == 0)
            {

                return i;
            }
        }

        return -1;
    }

    public void AddOnFirstEmptySlot(ItemStack stack)
    {
        int work = GetFirstEmptySlotIndex();
        if (work > -1 && work < slots.Count)
        {
            slots[work].AddItemInSlot(stack);
        }

    }

    //shift click item
    public void QuickAddItem(ItemStack stack)
    {
        TryMergeOnExisting(stack);
        AddOnFirstEmptySlot(stack);
    }


    public override void QuickSendStack(ItemStack stack, DragItem drag)
    {
        Debug.Log("QuickSendStack : CoffreUI");
        PlayerInventory  inv= GM.Joueur.BarreInventaire;
        inv.MergeOnExisting(stack);
        Debug.Log(drag);
        Slot temp = inv.GetFirstEmptySlot();
        if (temp != null) 
        { 
            temp.SwapItems(drag);
        }
    }

    public Slot GetFirstEmptySlot()
    {
        int ret = GetFirstEmptySlotIndex();
        if(ret != -1)
        {
            return slots[ret];
        }
        else
        {
            return null;
        }     
    }


}
