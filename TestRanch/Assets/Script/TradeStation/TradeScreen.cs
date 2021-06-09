using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeScreen : MonoBehaviour, IInteractible
{
    [SerializeField] GameObject TradePanel;

    //private List<Slot> slots;

    UIManager UI;
    GameManager GM;

    public void Interact(Player joueur)
    {
        UI.OpenPanel(TradePanel);
    }


    // Start is called before the first frame update
    void Start()
    {
        UI = UIManager.Instance;
        GM = GameManager.gmInstance;
      /*  slots = new List<Slot>();
        Slot[] temp = TradePanel.GetComponentsInChildren<Slot>();
        foreach (var item in temp)
        {
            slots.Add(item);
        }
      */
    }

    

}
