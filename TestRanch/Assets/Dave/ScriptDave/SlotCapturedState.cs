using Assets.Dave.ScriptDave;

internal class SlotCapturedState : State
{
    public SlotCapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
    {
        if (!CreatureBehavior.OnlyOnce)
        {
            CreatureBehavior.State1 = "CreatureInSlot";
            CreatureBehavior.CreatureInfoPanel.SetActive(false);
            CreatureBehavior.transform.position = CreatureBehavior.PokeballTransform.transform.position;
            CreatureBehavior.PlayerFound = false;
            //CreatureBehavior.targets = null;
            CreatureBehavior.Agent.canMove = false;
            CreatureBehavior.Agent.canSearch = false;
            CreatureBehavior.ListCreaturePokeBall.AddToList(CreatureBehavior);
        }

    }
}