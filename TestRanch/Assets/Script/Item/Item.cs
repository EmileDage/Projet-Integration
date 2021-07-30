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
    [SerializeField] protected AudioClip useSound;
    [SerializeField] protected GameObject itemWorldObject;

    #region getset
    public string Nom { get => nom;}
    public int MaxStack { get => maxStack;}
    public  Sprite Icon_Inventory { get => icon_Inventory;}
    public string Description { get => description;}
    public int Valeur { get => valeur;}
    public int ID { get => iD;}
    public  int InteractionBonusRange { get => interactionBonusRange;}
    public GameObject ItemWorldObject { get => itemWorldObject; }
    public AudioClip UseSound { get => useSound;}
    #endregion
    public virtual void UseThis(ItemStack itemStack, Player joueur) 
    {
        Debug.Log("used " + nom);
    }

    public virtual void OnSelecting(Player joueur) 
    {       
        Transform g = joueur.Equiped.transform;
        g.GetComponent<ItemToSee>().ItemRotation = itemWorldObject.transform.rotation.eulerAngles;
        g.localScale = itemWorldObject.transform.localScale;
        g.transform.rotation = itemWorldObject.transform.rotation;
        joueur.Equiped.GetComponent<MeshRenderer>().materials = itemWorldObject.GetComponent<MeshRenderer>().sharedMaterials;
        joueur.Equiped.mesh = itemWorldObject.GetComponent<MeshFilter>().sharedMesh;
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


