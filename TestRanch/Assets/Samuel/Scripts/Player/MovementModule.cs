using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementModule : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Stats speed;
    [SerializeField] private float dragForce = 6f;


    private Vector3 velocity;
    private Rigidbody rig = null;

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        Load();
        rig = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        DirectionalMovement();
        rig.drag = dragForce;
    }

    public Stats GetSpeed()
    {
        return speed;
    }
    public void ModifySpeed(float value, bool RemoveValue = false)
    {
        if(!RemoveValue)
        speed.AddModifier(value);
        else
            speed.RemoveModifier(value);

    }
    public void SetVelocityMovement(Vector3 velocityVector)
    {
        velocity = velocityVector;
    }
    private void DirectionalMovement()
    {
        //  controller.Move(velocity * speed.Value() * Time.deltaTime);
        rig.AddForce(velocity.normalized * speed.Value());
    }

    void Save()
    {
        SaveSystem.Save(transform.position, "PlayerPosition");
    }

    void Load()
    {
        if (SaveSystem.SaveExists("PlayerPosition"))
        {
            transform.position = SaveSystem.Load<Vector3>("PlayerPosition");
        }
    }
}
