using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradePanel : MonoBehaviour
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private List<Item> dispoInShop;//liste d'item que le shop offre au joueur
    private List<TradeProductUI> itemsInShop;
    /*
    private List<ItemStack> shoppingCart;*/
    private void Start()
    {
        //shoppingCart = new List<ItemStack>();
        itemsInShop = new List<TradeProductUI>();
    }
    

    public void Buy() { }

    public void OpenTradePanel()
    {
        foreach (var item in dispoInShop)
        {

        }
    }
}
