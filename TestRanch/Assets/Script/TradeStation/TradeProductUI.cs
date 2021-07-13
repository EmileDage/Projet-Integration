using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeProductUI : MonoBehaviour
{
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text priceTxt;
    [SerializeField] private Image itemImage;
    OnScreenMessage msg;
    public Button buyButton;
    private Item item;

    private string notEnoughMoneyMsg = "NOT ENOUGH MONEY";
    private string inventoryFullMsg = "NO INVENTORY SPACE";

    private GameManager gm;
    private UIManager ui;

    public void SetUp(Item item)
    {
        gm = GameManager.gmInstance;
        ui = UIManager.Instance;
        msg = ui.ScreenMsg.GetComponent<OnScreenMessage>();
        this.item = item;
        nameTxt.text = item.Nom;
        priceTxt.text = item.Valeur.ToString() + "$";
        itemImage.sprite = item.Icon_Inventory;
    }

    public void Buy()
    {
        Debug.Log("Buy");
        if(gm.GetChronoCoin() >= item.Valeur ) { 

            Debug.Log("enough coin");
            ItemStack stack = new ItemStack(item, 1);
            int temp = gm.Joueur.BarreInventaire.QuickAddItem(stack);
            Debug.Log(temp);
            if(temp == 0)
            {
                gm.ModifyChronoCoin(stack.Item.Valeur,true);
            }
            else
            {
                msg.StartCounter(inventoryFullMsg);
            }
            
            //bool edbug = gm.Joueur.BarreInventaire.TryMergeOnExisting(stack);
            //Debug.Log(edbug + "merge");
           /* if (left == 0)
            {
                Debug.Log("merging");
                gm.ModifyChronoCoin(item.Valeur, true);
            }else
            {
                if (gm.Joueur.BarreInventaire.GetFirstEmptySlotIndex() >= 0 && stack.Qte > 0) 
                {
                    gm.Joueur.BarreInventaire.AddOnFirstEmptySlot(stack);
                    gm.ModifyChronoCoin(item.Valeur, true);
                }  else
                {   
                    msg.StartCounter(inventoryFullMsg);
                }
            }*/
            
          
        }
        else
        {
            msg.StartCounter(notEnoughMoneyMsg);
        }
    }
    
}
