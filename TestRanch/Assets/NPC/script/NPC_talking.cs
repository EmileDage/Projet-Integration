using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_talking : MonoBehaviour, IInteractible
{

    //dialogue
    [SerializeField] protected DialogueManager manager;
    protected DialogueTrigger conversation;
    protected bool talked;//talked = false veut dire que le joueur doit demander la quest 



    private void Start()
    {
        conversation = this.gameObject.GetComponent<DialogueTrigger>();
    }

    public void Interact(Player joueur)//quand joueur interagit avec NPC
    {
        if (!talked)
        { //le joueur na pas parler au npc une premiere fois yet  
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueStart();
                talked = true;
            }

        }
        else{
            conversation.TriggerDialogueChat();
            talked = false;
        }
       
    }
}
