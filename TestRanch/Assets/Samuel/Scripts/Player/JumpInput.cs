using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInput : MonoBehaviour
{

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            GetComponent<JumpModule>().SetJumpVelocity();
    }
}
