using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameManager.gmInstance.Joueur;
        transform.position = player.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + 60, transform.position.z);
    }
    private void LateUpdate()
    {
        transform.position =new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
