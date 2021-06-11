using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeScreen : MonoBehaviour, IInteractible
{


    //private List<Slot> slots;

    UIManager UI;


    public void Interact(Player joueur)
    {
        UI.OpenPanel(UI.StationPanel);
        UI.StationPanel.GetComponent<TradePanel>().OpenTradePanel();
    }


    // Start is called before the first frame update
    void Start()
    {
        UI = UIManager.Instance;
        UI.StationPanel.SetActive(false);
    }

    

}
