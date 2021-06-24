using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamInventory : MonoBehaviour
{
   /*
    [SerializeField] private List<Slot> slotsList = new List<Slot>();

    public void AddItem(Item item, int amount)
    {
        bool hasItem = false;
        for (int i = 0; i < slotsList.Count; i++)
        {
            if (slotsList[i].Item == item)
            {
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            slotsList.Add(new Slot(item, amount));
        }
    }
   */

    /*[SerializeField] private Slot[] inventorySlots;
    [SerializeField] private int currentSlot = 0;
    [SerializeField] private Transform selection;
    private Input input;
    private void Start()
    {
        //foreach (Slot child in transform.GetComponentsInChildren<Slot>())
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            MoveLeft();
        if (Input.GetKeyDown(KeyCode.X))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ModeSelection(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ModeSelection(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ModeSelection(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            ModeSelection(3);

        if (Input.GetKeyDown(KeyCode.C))
            RemoveItemInSlot();
    }
    public void AddItemInSlot(T_Item item)
    {
        int i = 0;
        foreach (Slot slot in inventorySlots)
        {

            if (slot.isEmpty)
            {
                    UI_Inventory clone = Instantiate(item.inventoryItem, inventorySlots[i].transform.position,Quaternion.identity, inventorySlots[i].transform);
                    slot.AddItem(clone);
                    slot.isEmpty = false;
                   // item.PickedUp();
                    Debug.Log("Item Added");

                    break;
            }

            if(slot.item.ID == item.inventoryItem.ID)
            {
                if (slot.amountCurrent < slot.amountMaxium)
                {
                    slot.AddItem(item.inventoryItem);
                  //  item.PickedUp();
                    Debug.Log("Item Stacked");

                    break;
                }
                break;
            }

            i++;
        }
    }
    public void RemoveItemInSlot()
    {
        foreach (Slot slot in inventorySlots)
        {
            if (slot.item != null)
            {
                if(inventorySlots[currentSlot].item.ID == slot.item.ID)
                {
                slot.RemoveItem();
                break;
                }
            }
        }
    }
    private void MoveLeft()
    {
        if (currentSlot == 0)
            currentSlot = inventorySlots.Length - 1;
        else
            currentSlot -= 1;

        Selected();
    }
    private void MoveRight()
    {
        if (currentSlot == inventorySlots.Length -1)
            currentSlot = 0;
        else
            currentSlot += 1;

        Selected();
    }

    private void ModeSelection(int input)
    {
        currentSlot = input;
        Selected();
    }

    private void Selected()
    {
        selection.position = inventorySlots[currentSlot].transform.position;
    }*/
}
