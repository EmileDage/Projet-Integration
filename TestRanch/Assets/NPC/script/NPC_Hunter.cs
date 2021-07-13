using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Hunter : NPC_basicsRewards
{
    [SerializeField] private NPC_hunted Hunted;

    private void Start()
    {
        conversation = this.gameObject.GetComponent<DialogueTrigger>();
        chest.gameObject.SetActive(false);
        Hunted.gameObject.SetActive(false);

        for (int a = 0; a < rewards.Length; a++)//créer la liste avec des itemstacks
        {
            chest.Contenu[a] = new ItemStack(rewards[a], rewardsQte[a]);
        }

    }

    public override void Interact(Player joueur)
    {
        if (!talked) { //le joueur na pas parler au npc une premiere fois yet  
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueStart();
                Hunted.gameObject.SetActive(true);
                talked = true;

            }

        }
        else if (Quest_completed)
        {//la quete est faite mais le joueur for some reason veut parler au npc
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueIdleChat();
            }
        }
        else if (Hunted.Hunt)//Check if you have what the NPC WANTS
        {
            if (!manager.FadeOut)
            {
               
                conversation.TriggerDialogueEnd();
                chest.gameObject.SetActive(true);
                Quest_completed = true;
            }

        }
        else
        {//tu nas pas ce que le npc veut
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueWaiting();
            }
        }
    }
}
