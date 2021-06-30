using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Materiaux", menuName = "ScriptableItems/Materiaux", order = 2)]
public class Materiaux : Item
{
    [SerializeField] private Fonctions funct;

    [SerializeField]private int speed = 10;

    //seulement les materiaux qui sont utiliser pour setter un spawner dans les carrés on besoin d'un spawner assigner
    [SerializeField] private GameObject spawner;
    public Fonctions Funct { get => funct; }
    public int Speed { get => speed;}
    public GameObject Spawner { get => spawner; set => spawner = value; }

    public override void UseThis(ItemStack stack, Player joueur)
    {
        if (!stack.isEmpty())
        {
            base.UseThis(stack, joueur);
            GameObject spawn;
            spawn = Instantiate(this.itemWorldObject);
            spawn.transform.rotation = joueur.playerCam.transform.rotation;
            spawn.transform.position = joueur.Offset.position;
            stack.RemoveAmount(1);
            joueur.Selected.UpdateSlot();
            spawn.GetComponent<WorldObjectMateriaux>().Qte = 1;
            //spawn.GetComponent<WorldObjectMateriaux>().SetItem(this);
            spawn.GetComponent<Rigidbody>().AddForce(joueur.transform.TransformDirection(Vector3.forward) * speed, ForceMode.Impulse);
        }
    }

    public override void SpawnAsObject(ItemStack stack, Transform location)
    {
        base.SpawnAsObject(stack, location);
        if (itemWorldObject.GetComponent<WorldObjectMateriaux>() == null)
        {
            Debug.LogError("les matériaux doivent avoir le scripte WorldObject Materiaux");
        }
    }


}
