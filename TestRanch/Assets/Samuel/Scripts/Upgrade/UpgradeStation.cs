using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour, IInteractible
{
    [SerializeField] private Transform upgradePanel = null;
    private bool isOpen = false;
    [SerializeField] private CameraControl cameraControl = null;
    [SerializeField] private GameObject player = null;

    [Header("StationModifier")]
    [SerializeField] float detectionDistance = 7;

    private void Start()
    {
        player = GameObject.Find("Player");
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
        float distance = Vector3.Distance(transform.position, joueur.transform.position);
        if(distance <= detectionDistance)
        {
            if (isOpen)
            {
                ClosePanel();
                player.GetComponent<MovementModule>().ModifySpeed(-999,true);
            }
            else
            {
                OpenPanel();
                player.GetComponent<MovementModule>().ModifySpeed(-999);
            }
        }
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

}
