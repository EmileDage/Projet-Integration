using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credits : Brackeys
//https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager d_manager;

    private void Start()
    {
        d_manager = DialogueManager.d_instance;
    }

    public void TriggerDialogue() {
        d_manager.StartDialogue(dialogue);
    }
}
