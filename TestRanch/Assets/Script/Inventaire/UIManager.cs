using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public CoffreUI coffreUI;
    [SerializeField] private AudioSource soundsUI;
    [SerializeField] private AudioClip acceptClip;
    [SerializeField] private AudioClip invalidActionClip;
    [SerializeField] private AudioClip chestOpenClip;
    [SerializeField] private AudioClip inventoryItemClip;
    [SerializeField] private GameObject stationPanel;
    [SerializeField] private GameObject sellPanel;
    [SerializeField] private GameObject screenMsg;
    [SerializeField] private GameObject minimapCamObject;
    [SerializeField] private GameObject minimapUI;

    private CameraControl camJoueur;

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
        camJoueur = GameManager.gmInstance.Joueur.GetComponent<CameraControl>();
    }


    public void AcceptSound()
    {
        soundsUI.clip = acceptClip;
        soundsUI.Play();
    }

    public void InvalidActionSound()
    {
        soundsUI.clip = invalidActionClip;
        soundsUI.Play();
    }

    private void ChestSound()
    {
        soundsUI.clip = chestOpenClip;
        soundsUI.Play();
    }

    public void ItemSound()
    {
        soundsUI.PlayOneShot(inventoryItemClip);
    }

    public void ExitPanel(GameObject panel)
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camJoueur.UnlockCamera();
        panel.SetActive(false);
        Debug.Log("exit panel");
    }

    public void CloseChest(GameObject panel)
    {
        coffreUI.CloseChest();
        ChestSound();
        ExitPanel(panel);
    }

    public void OpenChestFromChest(Coffre chest)
    {
        ChestSound();
        coffreUI.SetUp(chest.Size, chest.Contenu, chest);
        coffreUI.OpenChest();
        ActivateMouse();
    }

    public void ActivateMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        //add camera stuff
    }

    public void OpenPanel(GameObject Panel) {
        Panel.SetActive(true);
        AcceptSound();
        ActivateMouse();
        camJoueur.LockCamera();
    }

    public void MapOpenClose()
    {
        minimapUI.SetActive(!minimapUI.activeSelf);
        minimapCamObject.SetActive(!minimapCamObject.activeSelf);
    }

    

}
