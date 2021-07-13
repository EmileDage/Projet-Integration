using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Fetch :  NPC_basicsRewards
{
    //Fetchthings
    [SerializeField] private Item[] fetchThis;//exemple bannane
    [SerializeField] private int[] fetchThisQte;//exemple 2 ainsi le npc veut 2 banane
    private List<ItemStack> list_Things_toFetch = new List<ItemStack>();

    private void Start()
    {
        conversation = this.gameObject.GetComponent<DialogueTrigger>();

        for (int a = 0; a < fetchThis.Length; a++)//créer la liste avec des itemstacks
        {
            list_Things_toFetch.Add(new ItemStack(fetchThis[a], fetchThisQte[a]));
        }

        chest.gameObject.SetActive(false);
        
        for (int a = 0; a < rewards.Length; a++)//créer la liste avec des itemstacks
        {
            chest.Contenu[a] = new ItemStack(rewards[a], rewardsQte[a]);
        }


    }

    public override void Interact(Player joueur)//quand joueur interagit avec NPC
    {
        if (!talked) { //le joueur na pas parler au npc une premiere fois yet  
              if (!manager.FadeOut)
            {
                conversation.TriggerDialogueStart();
                talked = true;
            }

        } else if (Quest_completed) {//la quete est faite mais le joueur for some reason veut parler au npc
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueIdleChat();
            }         
        }
        else if (joueur.BarreInventaire.TryPayWithMultipleItems(list_Things_toFetch))//Check if you have what the NPC WANTS
        {
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueEnd();
                chest.gameObject.SetActive(true);
                Quest_completed = true;
            }
           
        }
        else {//tu nas pas ce que le npc veut
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueWaiting();
            }
        }
    }

}
