using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WorldObjectMateriaux : WorldObject, IInteractible
{
    private Materiaux materiaux;


    public Materiaux Materiaux { get => materiaux; set => materiaux = value;}

    private void Start()
    {
        Debug.Log("Start WOM");
        materiaux = (Materiaux)this.item;
      //  Debug.Log("WorldObject Start" + materiaux);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        this.materiaux = (Materiaux)item;
    }

    public Materiaux SetItem()
    {
        this.materiaux = (Materiaux)this.item;
        return materiaux;
    }

    new public Materiaux Item()
    {
        return materiaux;
    }


    new public void Interact(Player joueur)
    {      
        ItemStack temp = new ItemStack(SetItem(), Qte);
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


}
