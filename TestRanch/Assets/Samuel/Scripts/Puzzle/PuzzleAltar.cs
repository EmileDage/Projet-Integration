using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAltar : MonoBehaviour, IInteractible
{
    [SerializeField] float detectionDistance = 7;
    private Player player = null;
    [SerializeField] private ParticleSystem fire = null;
    private bool isActive = false;

    private void Start()
    {
        player = GameManager.gmInstance.Joueur;
        fire.Stop();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact(player.GetComponent<Player>());
        }
    }
    public bool IsActivated()
    {
        return isActive;
    }

    public void Interact(Player joueur)
    {
        float distance = Vector3.Distance(transform.position, joueur.transform.position);
        if (distance <= detectionDistance && !isActive)
        {
            Debug.Log("Activated");
            isActive = true;
            fire.Play();
            PuzzleManager.puzzleManagerInstance.CheckIfAltarsAreCompleted();
        }
    }
}
