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
    [SerializeField] protected GameObject itemWorldObject;

    public string Nom { get => nom;}
    public int MaxStack { get => maxStack;}
    public  Sprite Icon_Inventory { get => icon_Inventory;}
    public string Description { get => description;}
    public int Valeur { get => valeur;}
    public int ID { get => iD;}
    public  int InteractionBonusRange { get => interactionBonusRange;}
    public GameObject ItemWorldObject { get => itemWorldObject; }


    public virtual void UseThis(ItemStack itemStack, Player joueur) 
    {
        Debug.Log("used " + nom);
    }

    

    //mouse button 2 - clique droit
    public virtual void AltUse(Player joueur)
    {

    }

    public virtual void OnSelecting(Player joueur) 
    {
        joueur.Equiped.mesh = itemWorldObject.GetComponent<MeshFilter>().sharedMesh;
        Transform g = joueur.Equiped.transform;
        g.localScale = new Vector3(itemWorldObject.transform.localScale.x, itemWorldObject.transform.localScale.y * 0.66f, itemWorldObject.transform.localScale.z);
        g.transform.rotation = itemWorldObject.transform.rotation;        
    }

    


    public virtual GameObject SpawnAsObject(ItemStack stack, Transform location)
    {
        Debug.Log("spawn " + nom);
        GameObject temp;
        temp = Instantiate(itemWorldObject, location.position, location.rotation);
        temp.GetComponent<WorldObject>().Qte = stack.Qte;
        temp.GetComponent<WorldObject>().Item = this;
        return temp;
        
    }
}


