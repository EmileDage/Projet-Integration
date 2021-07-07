using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [SerializeField] private Regen Health = null;
    [SerializeField] private Animator animator = null; 
    private bool isDead = false;

    void Start()
    {
        Health.InitializeRecovery();
    }

    void Update()
    {
        Health.StartRecovery();
    }

    public void ModifyHealth(float value, bool RemoveValue = false)
    {
        if (!RemoveValue)
            Health.AddModifier(value);
        else
            Health.RemoveModifier(value);
    }
    public void DecreaseHealth(float creatureDamage)
    {
        Health.DecreaseCurrentValue(creatureDamage);
        if (Health.GetCurrentValue() <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Death());
        }

    }
    public Regen GetHealth()
    {
        return Health;
    }

    private IEnumerator Death()
    {
        animator.Play("FadeIn");
        yield return new WaitForSeconds(1);
        GetComponent<RespawnModule>().Respawn();
        Health.IncreaseCurrentValue(Health.Value());
        animator.Play("FadeOut");
        isDead = false;

    }
}
