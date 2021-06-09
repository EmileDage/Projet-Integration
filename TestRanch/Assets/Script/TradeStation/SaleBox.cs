using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleBox : MonoBehaviour, IInteractible
{
    UIManager UI;

    private void Start()
    {
        UI = UIManager.Instance;
    }
    public void Interact(Player joueur)
    {
        UI.OpenPanel(UI.SellPanel);
    }
}
