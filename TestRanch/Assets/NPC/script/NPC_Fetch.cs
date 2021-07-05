using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Fetch : MonoBehaviour, IInteractible
{
    //Fetchthings
    [SerializeField] private Item[] fetchThis;//exemple bannane
    [SerializeField] private int[] fetchThisQte;//exemple 2
                                                       //ainsi le npc veut 2 banane
    private List<ItemStack> list_Things_toFetch = new List<ItemStack>();

    //rewards
    [SerializeField] private Item[] rewards;
    [SerializeField] private int[] rewardsQte;
    private List<ItemStack> rewards_list;
    [SerializeField] private Coffre chest;//le npc va juste drop un chest avec l



    //dialogue
    private DialogueTrigger conversation;
    private bool talked;//talked = false veut dire que le joueur doit demander la quest 
    private bool quest_completed;

    private void Start()
    {
        conversation = this.gameObject.GetComponent<DialogueTrigger>();

        for (int a = 0; a < fetchThis.Length; a++)//créer la liste avec des itemstacks
        {
            list_Things_toFetch.Add(new ItemStack(fetchThis[a], fetchThisQte[a]));
        }

        
        for (int a = 0; a < rewards.Length; a++)//créer la liste avec des itemstacks
        {
            rewards_list.Add(new ItemStack(rewards[a], rewardsQte[a]));
        }
        chest.Contenu = rewards_list;
        chest.gameObject.SetActive(false);
    }

    public void Interact(Player joueur)//quand joueur interagit avec NPC
    {

        if (!talked) { //le joueur na pas parler au npc une premiere fois yet
            conversation.TriggerDialogueStart();
            talked = true;

        } else if (quest_completed) {
            conversation.TriggerDialogueChat();
        }
        else if (joueur.BarreInventaire.TryPayWithMultipleItems(list_Things_toFetch))//Check if you have what the NPC WANTS
        {
            conversation.TriggerDialogueEnd();
            chest.gameObject.SetActive(true);
            quest_completed = true;
        }
        else {
            conversation.TriggerDialogueWaiting();
        }
    }
}
