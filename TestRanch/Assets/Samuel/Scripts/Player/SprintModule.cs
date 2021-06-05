using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintModule : MonoBehaviour
{
    [SerializeField] private float sprintSpeed = 30;
    private StaminaModule staminaModule;
    private bool isSprinting = false;
    private MovementModule movementModule;

    void Start()
    {
        movementModule = GetComponent<MovementModule>();
        staminaModule = GetComponent<StaminaModule>();
    }

    private void Update()
    {
        CheckIfExhausted();
    }

    public void ActivateSprintSpeed()
    {
        if (staminaModule.GetStamina().Value() >= 0 && !staminaModule.IsExhaust() && isSprinting == false)
        {
            movementModule.ModifySpeed(sprintSpeed);
            isSprinting = true;
        }
    }
    public void DesactivateSprintSpeed()
    {
        if (isSprinting)
        {
            movementModule.ModifySpeed(sprintSpeed, true);
            isSprinting = false;
        }
    }
    private void CheckIfExhausted()
    {
        if (staminaModule.GetStamina().GetCurrentValue() <= 0)
        {
            staminaModule.ActivateExhaust();
            DesactivateSprintSpeed();
        }
    }
}
