using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour, IInteractible
{
    [SerializeField]protected int qte;   
    [SerializeField]protected Item item;
    
    public int Qte { get => qte; set => qte = value; }
    public Item Item { get => item; set => item = value; }

    public void Interact(Player joueur)
    {
        ItemStack temp = new ItemStack(item, Qte);
        Debug.Log(temp + "temp item stack");
        joueur.BarreInventaire.MergeOnExisting(temp);
        Qte = temp.Qte;

        if (joueur.BarreInventaire.TryAddOnEmptySlot(temp))
        {
            Destroy(this.gameObject);
        }
        if (Qte == 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void RemoveQte(int amount)
    {
        Qte -= amount;
        if (qte == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void DecrementeQte()
    {
        qte--;
        if (qte == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
