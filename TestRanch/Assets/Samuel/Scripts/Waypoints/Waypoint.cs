using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour, IInteractible
{
    [SerializeField] private Transform spawnPosition = null;
    [SerializeField] private GameObject btn_Object = null;
    [SerializeField] private Transform UnlockedTextZone = null;
    [SerializeField] private string waypointText = "default";
    private bool isUnlocked = false;
    [SerializeField] float detectionDistance = 7;

    private void Start()
    {
        btn_Object.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact(GameManager.gmInstance.Joueur);
        }
    }

    public void TryTeleportThePlayer(Player player)
    {
        if(isUnlocked)
        player.transform.position = spawnPosition.position + new Vector3(0, 1, 0);
    }
    public void Interact(Player joueur)
    {
        float distance = Vector3.Distance(transform.position, joueur.transform.position);
        if (distance <= detectionDistance)
            UnlockWaypoint();
    }
    private void UnlockWaypoint()
    {
        btn_Object.SetActive(true);
        StartCoroutine(TemporaryVisual());
        isUnlocked = true;
    }
    private IEnumerator TemporaryVisual()
    {
        UnlockedTextZone.gameObject.SetActive(true);
        UnlockedTextZone.GetComponent<Text>().text = "Discover : " + waypointText + " Waypoint";

        yield return new WaitForSeconds(5);
        UnlockedTextZone.gameObject.SetActive(false);

    }

}
