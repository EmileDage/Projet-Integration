using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableItems/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] private string nom;
    [SerializeField] private int maxStack;//pour inventaire
    [SerializeField] private Sprite icon_Inventory;
    [SerializeField] private string description;
    [SerializeField] private int valeur;
    [SerializeField] private int iD;
    [SerializeField] protected int interactionBonusRange = 0;
    [SerializeField] protected float yieldModifier = 1;
    [SerializeField] protected GameObject itemWorldObject;

    public string Nom { get => nom;}
    public int MaxStack { get => maxStack;}
    public  Sprite Icon_Inventory { get => icon_Inventory;}
    public string Description { get => description;}
    public int Valeur { get => valeur;}
    public int ID { get => iD;}
    public  int InteractionBonusRange { get => interactionBonusRange;}
    public  float YieldModifier { get => yieldModifier;}

    public GameObject ItemWorldObject { get => itemWorldObject; }

    public virtual void UseThis(ItemStack itemStack, Player joueur) 
    {
        Debug.Log("used " + nom);
    }

    public virtual void SpawnAsObject(ItemStack stack, Transform location)
    {
        Debug.Log("spawn " + nom);
        GameObject temp;
        temp = Instantiate(itemWorldObject, location.position, location.rotation);
        temp.GetComponent<WorldObject>().Qte = stack.Qte;
        temp.GetComponent<WorldObject>().Item = this;
        
    }
}


