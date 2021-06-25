using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFabric : MonoBehaviour
{
    public int chainLength = 2;
    public Transform Target = null;
    public Transform Pole = null;

    public int Iterations = 10;
    public float Delta = 0.001f;
    [Range(0,1)]
    public float SnapeBackStrength = 1f;

    private void Start()
    {
        
    }
}
