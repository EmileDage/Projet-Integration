using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyMushroom : MonoBehaviour
{
    [SerializeField] private float bouncePower = 15;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * bouncePower, ForceMode.Impulse);
        }
    }
}
