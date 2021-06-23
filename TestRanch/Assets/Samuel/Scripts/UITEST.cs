using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITEST : MonoBehaviour
{
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

    }

}
