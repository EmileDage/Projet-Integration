using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_hunted : NPC_basicsRewards
{
    [SerializeField] private Transform spawn;
    private bool finished;
    public bool Hunt { get => Talked;}

    private void Start()
    {
        conversation = this.gameObject.GetComponent<DialogueTrigger>();
        chest.gameObject.SetActive(false);

    }

    public override void Interact(Player joueur)
    {


        if (!Talked)
        { //le joueur na pas parler au npc une premiere fois yet 
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueStart();
                Talked = true;              
            }

        }
        else  {//la quete est faite mais le joueur for some reason veut parler au npc
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueIdleChat();
            }
        }
       
    }


    private void OnTriggerExit(Collider other)
    {
        if (!manager.SomeoneIsTalking)
        {
            if (Talked)
            {
                if (other.tag == "Player")
                {
                    this.gameObject.transform.localPosition = spawn.localPosition;
                    Destroy(this.gameObject.GetComponent<BoxCollider>());
                }

            }
        }



    }

}
