using Assets.Dave.ScriptDave;

internal class SlotCapturedState : State
{
    public SlotCapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
    {
        CreatureBehavior.state = "CreatureInSlot";
        CreatureBehavior.transform.position = CreatureBehavior.pokeballTransform.transform.position;
        CreatureBehavior.targets = null;
        CreatureBehavior.agent.canMove = false;
        CreatureBehavior.agent.canSearch = false;
    }
}