using Assets.Dave.ScriptDave;
using System.Collections;
using UnityEngine;

internal class FoodSearchState : State
{
    public FoodSearchState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
    {
        if (CreatureBehavior.Agent.isStopped)
        {
            CreatureBehavior.Agent.isStopped = false;
        }
        if (CreatureBehavior.TargetCollider != null)
        {
            CreatureBehavior.Agent.destination = CreatureBehavior.TargetCollider.transform.position;
        }

        if (CreatureBehavior.CreatureInfo.hungry == "No")
        {
            //Drop Ressources
            CreatureBehavior.DropRessourceAnimal();
            CreatureBehavior.FoodFound = false;
        }

    }

}