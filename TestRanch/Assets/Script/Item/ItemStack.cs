using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack//iventaire
{
    private readonly Item item;
    int qte;
    private int ID;
    private List<Modifier> mods;

    #region constructeurs
    public ItemStack(Item item, int qte)
    {
        this.item = item;
        this.qte = qte;
        ID = item.ID;
    }
    public ItemStack(Item item)
    {
        this.item = item;
        this.qte = 0;
        ID = item.ID;
    }
    public ItemStack(ItemStack stack)
    {
        this.item = stack.item;
        this.qte = stack.qte;
        ID = item.ID;
    }
    #endregion

    public int Qte { get => qte; set => qte = value; }
    public Item Item { get => item;}

    public bool isEmpty()
    {
        if(qte <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isFull()
    {
        if (this.qte >= this.item.MaxStack)
        {
            return true;
        }
        return false;
    }

    public void UseItem(Player joueur)
    {
        item.UseThis(this, joueur);
    }

    public void RemoveAmount(int amount)
    {
        if(amount<= qte)
        {
            qte -= amount;
        }
    }
    
    //appel� lors d'un drag and drop //le update du slot est fait apr�s
    public bool TryMergeItemStack(ItemStack stackAdd)
    {

        if(this.item.ID == stackAdd.Item.ID && !isFull()) { 
            this.Qte += stackAdd.Qte;
            if (isFull())
            {
                //tester, fonctionne
                stackAdd.Qte = this.Qte - this.item.MaxStack;
                this.qte = this.item.MaxStack;
            }
            else
            {
                stackAdd.Qte = 0;
            }
            return true;
        }
        return false;
    }

    public ItemStack AddAmount(int qte)
    {
        ItemStack retour = null;
        if (this.qte >= this.item.MaxStack)
            return new ItemStack(this.item, qte);
        
        this.qte += qte;
        return retour;
    }

    public int GetValue()
    {
        return (item.Valeur * Qte);
    }

    public void ModifyItem()
    {

    }

    public float GetYieldModifier()
    {
        return 1;
    }

    public int GetInteractionRangeBonus()
    {
        return 0;
    }

    public void InstantiateRessourceObject(Transform location)
    {
        item.SpawnAsObject(this, location);
    }

    public bool CompareStack(ItemStack compared)
    {
        if (compared.item.ID == this.item.ID && this.Qte > compared.Qte)
        {
            return true;
        }
        return false;
    }
}

