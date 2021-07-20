using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dave.ScriptDave
{
    internal class AgressifState : State
    {
        public AgressifState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
        {
			CreatureBehavior.State1 = "Agressif";

			CreatureBehavior.Shoot();

			CreatureBehavior.Agent.destination = CreatureBehavior.Player.position;
			CreatureBehavior.Distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.Player.position);

			if (CreatureBehavior.Followdistance >= CreatureBehavior.Distance)
			{
				CreatureBehavior.Agent.isStopped = true;
				CreatureBehavior.CreatureInfoPanel.SetActive(true);
			}
			else
			{
				CreatureBehavior.Agent.isStopped = false;
				CreatureBehavior.CreatureInfoPanel.SetActive(false);
			}
			if (CreatureBehavior.Distance >= 17)
			{
				CreatureBehavior.PlayerFound = false;
			}
		}
        
    }
}