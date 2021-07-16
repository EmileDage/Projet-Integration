using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    public static WaypointsManager waypointManagerInstance;

    [SerializeField] private Player player;
    [SerializeField] private List<Waypoint> listOfWaypoints;
    private bool warpStoneUnlocked = false;

    void Awake()
    {

        if (waypointManagerInstance != null && waypointManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            waypointManagerInstance = this;
        }
    }
    public void Teleport(Waypoint waypoint)
    {
        if (warpStoneUnlocked)
            waypoint.TryTeleportThePlayer(player);
        else
            Debug.Log("You need the warpstone to teleport.");
    }
    public void UnlockWarpStone()
    {
        warpStoneUnlocked = true;
    }
}
