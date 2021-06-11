using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleBox : MonoBehaviour, IInteractible
{
    UIManager UI;
    GameManager gm;

    private void Start()
    {
        UI = UIManager.Instance;
        gm = GameManager.gmInstance;
        this.gameObject.SetActive(false);
    }
    public void Interact(Player joueur)
    {
        UI.OpenPanel(UI.SellPanel);
        gm.Joueur.OpenedNonChestInventory = UI.SellPanel.GetComponent<SalePanel>();
    }
}
