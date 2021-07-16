using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    private Animator animator = null;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (CheckKeyAmount() && !isOpen)
                ActivateDoor();
        }
    }
    public void ActivateDoor()
    {
        isOpen = true;
        GameManager.gmInstance.ModifyPuzzleKey(-1);
        animator.Play("OpenDoor");
    }
    private bool CheckKeyAmount() 
    {
        if (GameManager.gmInstance.GetPuzzleKey() > 0)
            return true;

        return false;
    }
}
