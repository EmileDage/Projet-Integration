using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler
{
    private ItemStack itemStack;
    [SerializeField] private Sprite box;
    [SerializeField] private Sprite lockedBox;
    [SerializeField] private Image selected;
    [SerializeField] private Image imgDrag;
    [SerializeField] private Text qteText;
    [SerializeField] private Item emptyItem;

    private AbstractInventoryUI parentUI;
    private DragItem dragItem;
    private Image slotImg;
    #region getset
    public Sprite Box { get => box;}
    public Sprite LockedBox { get => lockedBox;}
    public ItemStack ItemStack { get => itemStack; set => itemStack = value; }
    public DragItem DragItem { get => dragItem; set => dragItem = value; }
    public AbstractInventoryUI ParentUI { get => parentUI; set => parentUI = value; }
    public Item GetItemType() => this.itemStack.Item;
    #endregion
    private void Awake()
    {
        DragItem = imgDrag.GetComponent<DragItem>();
        DragItem.ParentSlot = this;
        slotImg = this.GetComponent<Image>();
        ItemStack = new ItemStack(emptyItem, 0);
        //UpdateSlot();
        DeSelect();
    }

    private bool IsOpen()
    {   
        return this.GetComponent<Image>().sprite.Equals(box);
    }

    public void OnDrop(PointerEventData eventData)
    {
        
       GameObject dragged= eventData.pointerDrag;
       DragItem drag = dragged.GetComponent<DragItem>();

       if( dragged != imgDrag.gameObject && drag != null && IsOpen())

        {
            
            DraggedItemMerge(drag);
            drag.ParentSlot.UpdateSlot();
        }
        
    }
    public void Select()
    {
        selected.gameObject.SetActive(true);
        Player joueur = GameManager.gmInstance.Joueur;
        if (!this.ItemStack.isEmpty())
        {
            joueur.Equiped.gameObject.SetActive(true);
            joueur.Selected.ItemStack.Item.OnSelecting(joueur);
        }
        else
        {
            joueur.Equiped.gameObject.SetActive(false);
        }
    }
    public void DeSelect()
    {
        selected.gameObject.SetActive(false);
    }
    public void DraggedItemMerge(DragItem dragged)
    {
        
            if(dragged.ParentSlot.ItemStack.Item.ID == this.itemStack.Item.ID ) 
            {
                this.TryMerge(dragged.ParentSlot.itemStack);
            } else
            {
                SwapItems(dragged);          
            } 
        
    }
    public void SwapItems(DragItem dragged)
    {

        ItemStack temp = dragged.ParentSlot.ItemStack;
        dragged.ParentSlot.ItemStack = this.itemStack;
        this.itemStack = temp;
        dragged.ParentSlot.UpdateSlot();
        this.UpdateSlot();
    }
    public void SetItemSprite(bool active, Sprite sprite)
    {
        imgDrag.sprite = sprite;
        imgDrag.gameObject.SetActive(active);
    }
    public void AddItemInSlot(ItemStack stack)
    {
        this.itemStack = stack;
        UpdateSlot();
    }
    public bool TryMerge(ItemStack stack)
    {

        if (this.itemStack.TryMergeItemStack(stack)) 
        {
            Debug.Log("Trymerge = true");
            UpdateSlot();
            return true;
        }
        return false;
    }
    public void UpdateSlot()
    {

        if(ItemStack.Qte > 0) {
            qteText.transform.parent.gameObject.SetActive(true);
            qteText.text = ItemStack.Qte.ToString();
            imgDrag.gameObject.SetActive(true);
            imgDrag.sprite = ItemStack.Item.Icon_Inventory;
            
        }
        else
        {
            itemStack = new ItemStack(emptyItem, 0);
            qteText.transform.parent.gameObject.SetActive(false);
            imgDrag.gameObject.SetActive(false);
            DragItem.ResetPosition();            
        }

        parentUI.UpdatePanel();
    }
    public void UpdateSlotWithoutPanel()
    {
        if (ItemStack.Qte > 0)
        {
            qteText.transform.parent.gameObject.SetActive(true);
            qteText.text = ItemStack.Qte.ToString();
            imgDrag.gameObject.SetActive(true);
            imgDrag.sprite = ItemStack.Item.Icon_Inventory;

        }
        else
        {
            qteText.transform.parent.gameObject.SetActive(false);
            imgDrag.gameObject.SetActive(false);
            DragItem.ResetPosition();
        }

        
    }
    public void QuickTransfer(DragItem drag)
    {
       // Debug.Log("QuickTransfer");
        parentUI.QuickSendStack(this.itemStack, drag);
        this.UpdateSlot();
    }
    public void RemoveItem()
    {
        this.itemStack = new ItemStack(emptyItem);

    }


}
