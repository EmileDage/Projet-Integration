using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlayer : MonoBehaviour
{
    [SerializeField] [Range(0, 999)]private int DmgGiven;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthModule>().DecreaseHealth(DmgGiven);
        }
    }
}
