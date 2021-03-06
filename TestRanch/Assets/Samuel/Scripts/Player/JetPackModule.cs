using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackModule : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 0.30f;
    private JumpModule jumpModule;
    private Rigidbody rig = null;
    private StaminaModule staminaModule;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        staminaModule = GetComponent<StaminaModule>();
        jumpModule = GetComponent<JumpModule>();
    }

    private void Update()
    {
        CheckIfExhausted();
    }

    public void ActivateFlight()
    {
        if (staminaModule.GetStamina().Value() >= 0 && !staminaModule.IsExhaust())
        {
            rig.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
            jumpModule.DesactivateGravity();
        }
    }
    public void DesactivateFlight()
    {
        jumpModule.ActivateGravity();
    }

    private void CheckIfExhausted()
    {
        if (staminaModule.GetStamina().GetCurrentValue() <= 0)
        {
            staminaModule.ActivateExhaust();
            DesactivateFlight();
        }
    }

}
