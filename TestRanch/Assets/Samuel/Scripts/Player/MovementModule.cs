using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementModule : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Stats speed;
    [SerializeField] private float dragForce = 6f;

    private bool isRoot = false;
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
    public void RootMovement()
    {
        rig.isKinematic = true;
        isRoot = true;
    }
    public void RemoveRootMovement()
    {
        rig.isKinematic = false;
        isRoot = false;
    }
    public bool IsRoot()
    {
        return isRoot;
    }
    private void DirectionalMovement()
    {
        rig.AddForce(velocity.normalized * speed.Value());
    }

    #region Saved
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
    #endregion
}
