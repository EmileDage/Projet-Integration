using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnModule : MonoBehaviour
{
    [SerializeField] private Transform initialRespawnPoint = null;
    private float delay = 0f;
    private float respawnCooldown = 1;
    private Transform currentRespawnPoint = null;

    public void Respawn()
    {
        if (currentRespawnPoint != null)
            transform.position = currentRespawnPoint.position;
        else
            transform.position = initialRespawnPoint.position;
    }

    public void SetCurrentRespawnPoint(Transform transform)
    {
        currentRespawnPoint = transform;
    }
    public void RemoveCurrentRespawnPoint()
    {
        currentRespawnPoint = null;
    }

}
