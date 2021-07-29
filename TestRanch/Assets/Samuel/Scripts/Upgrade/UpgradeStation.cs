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
       if (Input.GetButtonDown("Cancel"))
            ClosePanel();
    }
    public void Interact(Player joueur)
    {
            if (!isOpen)
                OpenPanel();
    }
    public void OpenPanel()
    {
        upgradePanel.gameObject.SetActive(true);
        cameraControl.LockCamera();
        player.GetComponent<MovementModule>().RootMovement();
        isOpen = true;
    }
    public void ClosePanel()
    {
        upgradePanel.gameObject.SetActive(false);
        cameraControl.UnLockCamera();
        player.GetComponent<MovementModule>().RemoveRootMovement();
        isOpen = false;
    }

}
