using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PuzzleDoor : MonoBehaviour
{
    private Animator animator = null;
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
                ActivateDoor();
        }
    }
    public void ActivateDoor()
    {
        GameManager.gmInstance.ModifyPuzzleKey(-1);
        animator.Play("OpenDoor");
        if(!audioPlay)
            this.GetComponent<AudioSource>().Play();
        audioPlay = true;
    }
    private bool CheckKeyAmount() 
    {
        if (GameManager.gmInstance.GetPuzzleKey() > 0)
            return true;

        return false;
    }
}
