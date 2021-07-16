using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dave.ScriptDave
{
    internal class AgressifState : State
    {
        public AgressifState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
        {
			CreatureBehavior.state = "Agressif";


			/*Vector3 lookVector = CreatureBehavior.player.transform.position - CreatureBehavior.transform.position;
			lookVector.y = CreatureBehavior.transform.position.y;
			Quaternion rot = Quaternion.LookRotation(lookVector);
			CreatureBehavior.transform.rotation = Quaternion.Slerp(CreatureBehavior.transform.rotation, rot, 1);*/

			CreatureBehavior.Shoot();

			CreatureBehavior.agent.destination = CreatureBehavior.player.position;
			CreatureBehavior.distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.player.position);

			if (CreatureBehavior.followdistance >= CreatureBehavior.distance)
			{
				CreatureBehavior.agent.isStopped = true;
				CreatureBehavior.creatureInfoPanel.SetActive(true);
			}
			else
			{
				CreatureBehavior.agent.isStopped = false;
				CreatureBehavior.creatureInfoPanel.SetActive(false);
			}
			if (CreatureBehavior.distance >= 17)
			{
				CreatureBehavior.playerFound = false;
			}
		}
        
    }
}