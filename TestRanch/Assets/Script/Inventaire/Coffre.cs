using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour, IInteractible
{
    // Start is called before the first frame update
    private List<ItemStack> contenu;//ce que le chest contient
    private UIManager UI_Instance;
    private GameManager GM_Instance;
    [SerializeField] private int size = 9;

    public List<ItemStack> Contenu { get => contenu; set => contenu = value; }
    public int Size { get => size; set => size = value; }



    public void Awake()
    {
        
        Contenu = new List<ItemStack>();

    }


    private void Start ()
    {
        UI_Instance = UIManager.Instance;
        GM_Instance = GameManager.gmInstance;
        for (int i = contenu.Count; i < size; i++)
        {
            Contenu.Add(GM_Instance.emptyItemItemStack);
        }
        if (true)//pour la sauvegarde
        {
            
        }
        else
        {
            //si tu load un save
        }
    }

    public void OpenChest()
    {
        UI_Instance.OpenChestFromChest(this);
    }


    public void Interact(Player joueur)
    {
        OpenChest();
        joueur.OpenChest = this;
    }

    public CoffreUI GetCoffreUI()
    {
        return UI_Instance.coffreUI;
    }

   
    public int GiveItemOfFonction(int Qte, Fonctions fonct)
    {
        int qteWork = Qte;//ce qu'il reste a envoyer
        int retour = 0;//la qte totale atteinte
        for (int i = 0; i < contenu.Count; i++)
        {
            Materiaux mat = (Materiaux)contenu[i].Item;
            if (mat !=null)
            {
                if (mat.Funct.Equals(fonct)) 
                {
                    if(contenu[i].Qte >= qteWork)
                    {
                        contenu[i].Qte -= qteWork;

                        if(contenu[i].Qte == 0)
                        {
                            contenu[i] = GM_Instance.emptyItemItemStack;
                        }
                        return Qte;

                    }
                    else
                    {
                        retour += contenu[i].Qte;
                        contenu[i] = GM_Instance.emptyItemItemStack;//remove item
                        qteWork -= contenu[i].Qte;
                    }
                }
            }
        }
        return retour;
    }


    public void IncreaseSize(int newSize)
    {
        for(int i = 0; i< (newSize-size); i++)
        {
            contenu.Add(GM_Instance.emptyItemItemStack);
        }
        size = newSize;
    }

    
}
