using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PuzzleDoor : MonoBehaviour
{
    private Animator animator = null;
    private bool isOpen = false;
    bool audioPlay = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (CheckKeyAmount())
            {
                ActivateDoor();
            }
        }
    }
    public void ActivateDoor()
    {
        if (isOpen)
            return;

        GameManager.gmInstance.ModifyPuzzleKey(-1);
        animator.Play("OpenDoor");
        if(!audioPlay)
            this.GetComponent<AudioSource>().Play();
        audioPlay = true;
        isOpen = true;

    }
    private bool CheckKeyAmount() 
    {
        if (GameManager.gmInstance.GetPuzzleKey() > 0)
            return true;

        return false;
    }
}
