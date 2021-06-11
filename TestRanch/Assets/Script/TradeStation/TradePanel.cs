using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradePanel : MonoBehaviour
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private GameObject productPanel;
    [SerializeField] private List<Item> dispoInShop;//liste d'item que le shop offre au joueur

    GameManager gm;
    UIManager ui;
    //private List<TradeProductUI> itemsInShop;
    /*
    private List<ItemStack> shoppingCart;*/
    private void Start()
    {
        //shoppingCart = new List<ItemStack>();
        //itemsInShop = new List<TradeProductUI>();
        gm = GameManager.gmInstance;
        ui = UIManager.Instance;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CloseTradePanel();
            ui.ExitPanel(this.gameObject);
        }
    }

    public void CloseTradePanel() 
    {
        TradeProductUI[] temp = GetComponentsInChildren<TradeProductUI>();
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i].gameObject);
        }
    }

    public void OpenTradePanel()
    {
        for (int i = 0; i < dispoInShop.Count; i++)
        {
            GameObject temp = Instantiate(productPrefab);
            temp.transform.SetParent(productPanel.transform,false);
            temp.GetComponent<TradeProductUI>().SetUp(dispoInShop[i]);
        }
    }
}
/*        slots.Add(Instantiate(slotPrefab).GetComponent<Slot>());
        slots[i].transform.SetParent(this.transform, false);
        slots[i].DragItem.Canvas = canvas;
        slots[i].DeSelect();
        SetSlotsParent(slots[i]);*/