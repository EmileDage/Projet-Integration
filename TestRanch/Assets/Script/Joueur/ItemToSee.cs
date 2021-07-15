using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSee : MonoBehaviour
{
    [SerializeField] private Transform look;

    private void Update()
    {
        transform.LookAt(look);
        transform.Rotate(-90, 0, 0);
    }
}
