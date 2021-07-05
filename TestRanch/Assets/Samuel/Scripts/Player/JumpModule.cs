using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpState {Jump, Fall, Flying, Swim }

public class JumpModule : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float fallSpeed = -5;

    private bool useGravity = true;
    private float groundDistance = 1.8f;
    private bool isGrounded = false;
    private Vector3 velocity;
    private Rigidbody rig = null;


    void Start()
    {
        rig = GetComponent<Rigidbody>();
        velocity.y = -9.81f;
    }
    void Update()
    {
        CheckIfGrounded();
        GravityForce();
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    public void ActivateGravity()
    {
        useGravity = true;
    }
    public void DesactivateGravity()
    {
        useGravity = false;
    }
    public void SetJumpVelocity()
    {
        if (isGrounded)
            rig.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
    }

    private void GravityForce()
    {
        if (!useGravity)
            return;

        if (isGrounded && velocity.y < 0)
            velocity.y = -9.81f;

        if(!isGrounded)
        velocity.y += -9.81f * fallSpeed * Time.deltaTime;

        rig.AddForce(velocity);
    }
    private void CheckIfGrounded()
    {
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
         isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);
         Vector3 down = transform.TransformDirection(Vector3.down) * groundDistance;
         Debug.DrawRay(transform.position, down, Color.red);
    }

}
