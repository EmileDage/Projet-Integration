using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;
    [SerializeField] private Player joueur;
    [SerializeField] private Item emptyItem;
    [HideInInspector]public ItemStack emptyItemItemStack;
    [SerializeField] private int chronoCoin = 0;
    [SerializeField] private int puzzleKey = 0;
    public Player Joueur { get => joueur;}


    void Awake()
    {
        if (gmInstance != null && gmInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gmInstance = this;
        }
        emptyItemItemStack = new ItemStack(emptyItem, 0);
    }


    public PlayerInventory GetPlayerInventory()
    {
        return joueur.BarreInventaire;
    }

    

    public void ModifyChronoCoin(int value, bool RemoveValue = false)
    {
        if (RemoveValue)
            chronoCoin -= value;
        else
            chronoCoin += value;
    }

    public void ModifyPuzzleKey(int value, bool RemoveValue = false)
    {
        if (RemoveValue)
            puzzleKey -= value;
        else
            puzzleKey += value;
    }


    public int GetChronoCoin()
    {
        return chronoCoin;
    }
    public int GetPuzzleKey()
    {
        return puzzleKey;
    }

    public void RootPlayer()
    {
        joueur.GetComponent<MovementModule>().RootMovement();
    }

    public void UnRootPlayer()
    {
        joueur.GetComponent<MovementModule>().RemoveRootMovement();
    }
}
