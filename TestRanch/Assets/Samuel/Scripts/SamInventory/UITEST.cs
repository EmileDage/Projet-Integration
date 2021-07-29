using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITEST : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject codexPanel = null;
    [SerializeField] private GameObject creaturesPanel = null;
    [SerializeField] private GameObject pauseMenu = null;


    [Header("Player")]
    public Image staminaBar = null;
    public Image healthBar = null;
    public Text txt_ChronoCoin = null;
    public Text txt_Keys = null;
    

    public Transform player = null;
    private StaminaModule staminaModule = null;
    private HealthModule healthModule = null;

    private void Start()
    {
        staminaModule = player.GetComponent<StaminaModule>();
        healthModule = player.GetComponent<HealthModule>();
    }

    private void Update()
    {
        staminaBar.fillAmount = staminaModule.GetStamina().GetCurrentValue() / staminaModule.GetStamina().Value();
        healthBar.fillAmount = healthModule.GetHealth().GetCurrentValue() / healthModule.GetHealth().Value();
        txt_ChronoCoin.text = GameManager.gmInstance.GetChronoCoin() + "Cc";
        txt_Keys.text = GameManager.gmInstance.GetPuzzleKey() + " Keys";

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.GetComponentInChildren<CameraControl>().UnlockCursor();
            OpenCodex();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerPauseMenu();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            player.GetComponentInChildren<CameraControl>().LockCursor();
            ClosePanel();
        }

    }

    public void OpenCodex()
    {
        if (creaturesPanel.activeSelf)
            creaturesPanel.SetActive(false);
        
            codexPanel.SetActive(true);
    }
    public void OpenCreatures()
    {
        if (codexPanel.activeSelf)
            codexPanel.SetActive(false);
        
            creaturesPanel.SetActive(true);
    }

    public void TriggerPauseMenu()
    {
        if(pauseMenu != null)
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                player.GetComponentInChildren<CameraControl>().LockCursor();
            }
            else
            {
                pauseMenu.SetActive(true);
                player.GetComponentInChildren<CameraControl>().UnlockCursor();
            }
        }
    }
    public void ClosePanel()
    {
        codexPanel.SetActive(false);
        creaturesPanel.SetActive(false);
        player.GetComponentInChildren<CameraControl>().LockCursor();

    }

}
