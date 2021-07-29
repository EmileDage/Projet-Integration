using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Npc_Basic : MonoBehaviour, IInteractible
{
    //dialogue
    [SerializeField] protected DialogueManager manager;
    protected DialogueTrigger conversation;
    protected bool talked;//talked = false veut dire que le joueur doit demander la quest 
    protected bool quest_completed;

    public bool Quest_completed { get => quest_completed; set => quest_completed = value; }
    public bool Talked { get => talked; set => talked = value; }

    public abstract void Interact(Player joueur);
}
