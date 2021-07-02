using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
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
        gameObject.SetActive(false);
    }
    private bool CheckKeyAmount() 
    {
        if (GameManager.gmInstance.GetPuzzleKey() > 0)
            return true;

        return false;
    }
}
