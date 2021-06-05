using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Dave.ScriptDave
{
    internal class PacifiqueState : State
    {
        public PacifiqueState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
        {
			CreatureBehavior.state = "Pacifique";
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
			if (CreatureBehavior.distance >= 15)
			{
				CreatureBehavior.playerFound = false;
			}
		}
    }
}