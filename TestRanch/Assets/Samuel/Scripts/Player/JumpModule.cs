using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpModule : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float fallSpeed = -5;

    private bool isFlying = false;
    private float groundDistance = 1.8f;
    private bool isGrounded;
    private Vector3 velocity;
    private Rigidbody rig = null;


    void Start()
    {
        rig = GetComponent<Rigidbody>();
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
    public void ActivateFlying()
    {
        isFlying = true;
    }
    public void DesactivateFlying()
    {
        isFlying = false;
    }
    public void SetJumpVelocity()
    {
        if (isGrounded)
            rig.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
    }

    private void GravityForce()
    {
        if (isFlying)
            return;

        if (isGrounded && velocity.y < 0)
            velocity.y = fallSpeed;

        if(!isGrounded)
        velocity.y += gravity * Time.deltaTime;

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
