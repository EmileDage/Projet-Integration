using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour, IInteractible
{
  //  [SerializeField] private List<Upgrade> upgradesList = new List<Upgrade>();
    [SerializeField] private Transform upgradePanel = null;
    private bool isOpen = false;
    [SerializeField] private CameraControl cameraControl = null;
    private GameObject player = null; 

    private void Start()
    {
        player = GameObject.Find("Player");
        //cameraControl = player.GetComponent<CameraControl>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Interact(player.GetComponent<Player>());
        }
    }
    public void Interact(Player joueur)
    {
        if (isOpen)
            ClosePanel();
        else
            OpenPanel();
    }
    public void OpenPanel()
    {
        upgradePanel.gameObject.SetActive(true);
        cameraControl.LockMouse();
        isOpen = true;
    }
    public void ClosePanel()
    {
        upgradePanel.gameObject.SetActive(false);
        cameraControl.UnlockMouse();
        isOpen = false;
    }

    private bool CheckChronoCoin(int value)
    {
        if ((GameManager.gmInstance.GetChronoCoin() - value) >= 0)
            return true;
        else return false;
    }

    #region Movement Upgrade
    public void UpdradeSpeedI(Upgrade upgrade)
    {
        if (!CheckChronoCoin(upgrade.GetCost()))
            return;

        if(!upgrade.IsActivated())
        {
            upgrade.Activate();
            player.GetComponent<MovementModule>().ModifySpeed(20);
            GameManager.gmInstance.ModifyChronoCoin(upgrade.GetCost(), true);
        }
    }
    public void UpdradeSpeedII(Upgrade upgrade)
    {
        if (!CheckChronoCoin(upgrade.GetCost()))
            return;

        if (!upgrade.IsActivated())
        {
            upgrade.Activate();
            player.GetComponent<MovementModule>().ModifySpeed(20);
            GameManager.gmInstance.ModifyChronoCoin(upgrade.GetCost(), true);
        }
    }
    public void UpdradeSpeedIII(Upgrade upgrade)
    {
        if (!CheckChronoCoin(upgrade.GetCost()))
            return;

        if (!upgrade.IsActivated())
        {
            upgrade.Activate();
            player.GetComponent<MovementModule>().ModifySpeed(20);
            GameManager.gmInstance.ModifyChronoCoin(upgrade.GetCost(), true);
        }
    }
    #endregion

    public void UpdradeJetPack(Upgrade upgrade)
    {
        if (!CheckChronoCoin(upgrade.GetCost()))
            return;

        if (!upgrade.IsActivated())
        {
            upgrade.Activate();
            player.AddComponent<JetPackInput>();
            player.AddComponent<JetPackModule>();
            GameManager.gmInstance.ModifyChronoCoin(upgrade.GetCost(), true);
        }
    }

}
