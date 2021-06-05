using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour, IInteractible
{
    private int qte;   
    protected Item item;
    public int Qte { get => qte; set => qte = value; }
    public Item Item { get => item; set => item = value; }

    public void Interact(Player joueur)
    {
        ItemStack temp = new ItemStack(item, Qte);
        joueur.BarreInventaire.QuickAddItem(temp);
        if (temp.Qte <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Qte = temp.Qte;
        }
    }
}
