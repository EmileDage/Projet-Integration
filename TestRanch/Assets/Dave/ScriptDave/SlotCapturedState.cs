using Assets.Dave.ScriptDave;

internal class SlotCapturedState : State
{
    public SlotCapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
    {
        CreatureBehavior.transform.position = CreatureBehavior.pokeballTransform.transform.position;
    }
}