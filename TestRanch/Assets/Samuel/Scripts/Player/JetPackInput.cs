using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            GetComponent<JetPackModule>().ActivateFlight();
            GetComponent<StaminaModule>().ActivateStaminaUse();
            GetComponent<StaminaModule>().DecreaseStamina();
        }

        if (Input.GetButtonUp("Jump"))
        {
            GetComponent<JetPackModule>().DesactivateFlight();
            GetComponent<StaminaModule>().DesactivateStaminaUse();
        }
    }
}
