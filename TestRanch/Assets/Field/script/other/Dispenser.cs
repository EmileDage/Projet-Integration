using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dispenser : MonoBehaviour
{
    [SerializeField] protected GameObject moving_visual;
    [SerializeField] [Range(0, 100)] private int qte_level;//quantite en pourcentage 
    [SerializeField] protected bool upgrade;
    [SerializeField] protected Enclos enclos_ref;
    protected int qteToConsume;
    protected Vector3 temp;//this temporary variable help me change the position since i cant directly access and modify x y z

    public int Qte_level { get => qte_level; set => qte_level = value; }
    public bool Upgrade { get => upgrade; set => upgrade = value; }
    public int QteToConsume { get => qteToConsume; }

    public virtual void Start()
    {
        temp = moving_visual.transform.localPosition;
        upgrade = false;

        if (Qte_level == 0)
            Empty();
        else
            SetLevel(Qte_level);

        qteToConsume = 10;//dans food dispenser il se fait modifier pour 3
    }

    public abstract void SetLevel(int pourcentage_desirer);

    public abstract void OnUpgrade();

    public abstract void Empty();//quand tu empty en food tu enleve aussi la reference du fruit/evggy

    public abstract void Consumme();//Drink/eat/hydrate


}
