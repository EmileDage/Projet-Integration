using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public CoffreUI coffreUI;
    [SerializeField] private GameObject stationPanel;
    [SerializeField] private GameObject sellPanel;
    [SerializeField] private GameObject screenMsg;
    [SerializeField] private GameObject minimapCamObject;
    [SerializeField] private GameObject minimapUI;
    private void Awake()
    {
        instance = this;
    }
    public static UIManager Instance { get => instance;}
    public GameObject StationPanel { get => stationPanel; }
    public GameObject SellPanel { get => sellPanel;}
    public GameObject ScreenMsg { get => screenMsg; set => screenMsg = value; }

    private void Start()
    {
        stationPanel.SetActive(false);
        SellPanel.SetActive(false);
        screenMsg.SetActive(false);
        minimapCamObject.SetActive(false);
        minimapUI.SetActive(false);
    }

    public void ExitPanel(GameObject panel)
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        Debug.Log("exit panel");
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
        ActivateMouse();
    }

    public void ActivateMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OpenPanel(GameObject Panel) {
        Panel.SetActive(true);
        ActivateMouse();
    }

    public void MapOpenClose()
    {
        minimapUI.SetActive(!minimapUI.activeSelf);
        minimapCamObject.SetActive(!minimapCamObject.activeSelf);
    }

    

}
