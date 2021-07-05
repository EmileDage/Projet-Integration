using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    [Header("Float Attributes")]
    [SerializeField] private float depthBeforeSubmerged = 1;
    [SerializeField] private float displacementAmount = 3f;

    private void FixedUpdate()
    {
        if ( transform.position.y < Ocean.oceanInstance.GetWaterLevel())
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rig.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
        }
    }
}
