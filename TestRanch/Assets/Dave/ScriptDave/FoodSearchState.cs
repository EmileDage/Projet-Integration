using Assets.Dave.ScriptDave;
using System.Collections;
using UnityEngine;

internal class FoodSearchState : State
{
    public FoodSearchState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
    {
        if (CreatureBehavior.agent.isStopped)
        {
            CreatureBehavior.agent.isStopped = false;
        }
        if (CreatureBehavior.targetCollider != null)
        {
            CreatureBehavior.agent.destination = CreatureBehavior.targetCollider.transform.position;
        }

        if (CreatureBehavior.creatureInfo.hungry == "No")
        {
            //Drop Ressources
            CreatureBehavior.DropRessourceAnimal();
            CreatureBehavior.foodFound = false;
        }

    }

}