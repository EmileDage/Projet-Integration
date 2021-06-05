using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementInput : MonoBehaviour
{
    void Update()
    {
       float x = Input.GetAxisRaw("Horizontal");
       float z = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = transform.forward * z + transform.right * x;
       
        GetComponent<MovementModule>().SetVelocityMovement(moveVector);
    }
}
