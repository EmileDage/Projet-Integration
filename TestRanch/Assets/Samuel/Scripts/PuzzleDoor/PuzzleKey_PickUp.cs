using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey_PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        GameManager.gmInstance.ModifyPuzzleKey(1);
        gameObject.SetActive(false);
    }

}
