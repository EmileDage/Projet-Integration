using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dispenser : MonoBehaviour
{
    [SerializeField] protected GameObject moving_visual;
    [SerializeField] [Range(0, 100)] private int qte_level;//quantite en pourcentage 
    [SerializeField] protected bool upgrade;
    protected Vector3 temp;//this temporary variable help me change the position since i cant directly access and modify x y z

    public int Qte_level { get => qte_level; set => qte_level = value; }

    public virtual void Start()
    {
        temp = moving_visual.transform.localPosition;
        upgrade = false;

        if (Qte_level == 0)
            Empty();
        else
            SetLevel(Qte_level);
    }

    public abstract void SetLevel(int pourcentage_desirer);

    public abstract void OnUpgrade();

    public abstract void Empty();//quand tu empty en food tu enleve aussi la reference du fruit/evggy

    public abstract void Consumme();//Drink/eat/hydrate


}
