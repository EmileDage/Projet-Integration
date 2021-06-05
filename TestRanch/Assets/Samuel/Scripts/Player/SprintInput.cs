using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            GetComponent<SprintModule>().ActivateSprintSpeed();
            GetComponent<StaminaModule>().ActivateStaminaUse();
        }

        if (Input.GetButtonUp("Sprint"))
        {
            GetComponent<SprintModule>().DesactivateSprintSpeed();
            GetComponent<StaminaModule>().DesactivateStaminaUse();
        }

        if (Input.GetButton("Sprint"))
            GetComponent<StaminaModule>().DecreaseStamina();


    }

}
