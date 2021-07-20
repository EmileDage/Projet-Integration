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
			CreatureBehavior.State1 = "Afraid";

			CreatureBehavior.Distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.Player.position);
			if (CreatureBehavior.Followdistance >= CreatureBehavior.Distance)
			{
				Vector3 dirToPlayer = (CreatureBehavior.transform.position - CreatureBehavior.Player.transform.position) * 2;
				Vector3 newPos = CreatureBehavior.transform.position + dirToPlayer;
				CreatureBehavior.Agent.destination = newPos;
			}
			else if (CreatureBehavior.Distance >= 12)
			{
				CreatureBehavior.PlayerFound = false;
			}
		}
    }
}