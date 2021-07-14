using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSee : MonoBehaviour
{
    [SerializeField] private Transform look;

    private void Update()
    {
        transform.LookAt(look, Vector3.up);
        transform.Rotate(-90, 0, 0);
    }
}
