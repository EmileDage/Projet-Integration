using Assets.Dave.ScriptDave;
using UnityEngine;

internal class SlotCapturedState : State
{
    public SlotCapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
    {
        if (!CreatureBehavior.OnlyOnce)
        {
            CreatureBehavior.State = "Creature In Inventory";
            CreatureBehavior.CreatureInfoPanel.SetActive(false);
            CreatureBehavior.transform.position = CreatureBehavior.PokeballTransform.transform.position;
            Debug.Log("playerfound");
            CreatureBehavior.PlayerFound = false;
            Debug.Log("playerfound2");
            //CreatureBehavior.targets = null;
            CreatureBehavior.Agent.canMove = false;
            CreatureBehavior.Agent.canSearch = false;
            CreatureBehavior.ListCreaturePokeBall.AddToList(CreatureBehavior);
        }

    }
}