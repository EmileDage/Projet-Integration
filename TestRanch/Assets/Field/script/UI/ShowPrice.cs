using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowPrice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text price_text_ref;//la reference au texte qui apparait sur l'ecran
    private string price_of_upgrade;//ce que l'on veut qui s'affiche comme prix
    [SerializeField] private Item[] price;
    [SerializeField] private int[] qte;
    [SerializeField] private int chronoCoinPrice;

    private List<ItemStack> liste = new List<ItemStack>();

    public List<ItemStack> Liste_prix { get => liste; }//appeler dans field_ui
    public int ChronoCoinPrice { get => chronoCoinPrice; }

    private void Start()
    {
        if (price.Length <= 0 && chronoCoinPrice <= 0)
        {
            price_of_upgrade = "Free until price is assigned";
        }
        else {
            PriceInString();
        }

        PutStuffInList();
    }

    private void PutStuffInList() {
        for (int a = 0; a < price.Length; a++)
        {
            liste.Add(new ItemStack(price[a], qte[a]));
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
   {
        price_text_ref.text = price_of_upgrade;
   }

    public void OnPointerExit(PointerEventData eventData)
    {
        price_text_ref.text = "";
    }


    public void PriceInString() {
        price_of_upgrade = "";//reset

        for (int a = 0; a < price.Length; a++) {
            //price_of_upgrade += Qte[a] +" "+ price[a].Nom+",";
            price_of_upgrade += price[a].Nom + " " + qte[a] + " , ";
        }
        if(chronoCoinPrice > 0)
        price_of_upgrade += chronoCoinPrice.ToString() + "cc." ;
    }
}
