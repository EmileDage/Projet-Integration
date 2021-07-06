using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC_basics : MonoBehaviour, IInteractible
{
    //rewards
    [SerializeField] protected Item[] rewards;
    [SerializeField] protected int[] rewardsQte;
    [SerializeField] protected Coffre chest;//le npc va juste drop un chest avec l

    //dialogue
    [SerializeField] protected DialogueManager manager;
    protected DialogueTrigger conversation;
    protected bool talked;//talked = false veut dire que le joueur doit demander la quest 
    protected bool quest_completed;

    public abstract void Interact(Player joueur);

}
