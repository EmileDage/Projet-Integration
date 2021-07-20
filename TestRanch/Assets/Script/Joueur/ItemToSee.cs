using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSee : MonoBehaviour
{
    [SerializeField] private Transform look;

    public Vector3 ItemRotation;

    private void Update()
    {
        transform.LookAt(look);
        transform.Rotate(ItemRotation);
    }
}
