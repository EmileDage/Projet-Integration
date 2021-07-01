using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        HealthModule joueur = collision.gameObject.GetComponent<HealthModule>();
        if (true) { }
    }
}
