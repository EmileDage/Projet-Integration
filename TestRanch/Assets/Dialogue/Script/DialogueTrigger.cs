using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credits : Brackeys
//https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue_startquest;
    public Dialogue dialogue_waitingquest;
    public Dialogue dialogue_endquest;
    public Dialogue dialogue_idlechat;
    private DialogueManager d_manager;

    private void Start()
    {
        d_manager = DialogueManager.d_instance;
    }

    public void TriggerDialogueStart() {
        d_manager.StartDialogue(dialogue_startquest);
    }

    public void TriggerDialogueWaiting()
    {
        d_manager.StartDialogue(dialogue_waitingquest);
    }

    public void TriggerDialogueEnd()
    {
        d_manager.StartDialogue(dialogue_endquest);
    }

    public void TriggerDialogueChat()
    {
        d_manager.StartDialogue(dialogue_idlechat);
    }
}
