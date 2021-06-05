using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public CoffreUI coffreUI;

    private void Awake()
    {
        instance = this;
    }
    public static UIManager Instance { get => instance;}

    public void ExitPanel(GameObject panel)
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
    }

    public void CloseChest(GameObject panel)
    {
        coffreUI.CloseChest();
        ExitPanel(panel);
    }

    public void OpenChestFromChest(Coffre chest)
    {
        coffreUI.SetUp(chest.Size, chest.Contenu, chest);
        coffreUI.OpenChest();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

}
