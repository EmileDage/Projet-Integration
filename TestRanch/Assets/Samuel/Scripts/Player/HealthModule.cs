using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [SerializeField] private Regen Health = null;

    // Start is called before the first frame update
    void Start()
    {
        Health.InitializeRecovery();
    }

    // Update is called once per frame
    void Update()
    {
        Health.StartRecovery();
    }

    public void DecreaseHealth(float creatureDamage)
    {
        Health.DecreaseCurrentValue(creatureDamage);
    }

    public Regen GetHealth()
    {
        return Health;
    }
}
