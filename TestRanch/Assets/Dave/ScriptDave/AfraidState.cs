using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Dave.ScriptDave

{
    internal class AfraidState : State
    {
        public AfraidState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
        {
			CreatureBehavior.state = "Afraid";

			CreatureBehavior.distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.player.position);
			if (CreatureBehavior.followdistance >= CreatureBehavior.distance)
			{
				Vector3 dirToPlayer = (CreatureBehavior.transform.position - CreatureBehavior.player.transform.position) * 2;
				Vector3 newPos = CreatureBehavior.transform.position + dirToPlayer;
				CreatureBehavior.agent.destination = newPos;
			}
			else if (CreatureBehavior.distance >= 12)
			{
				CreatureBehavior.playerFound = false;
			}
		}
    }
}