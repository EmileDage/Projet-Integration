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
			CreatureBehavior.State = "Pacifique";
			CreatureBehavior.Agent.destination = CreatureBehavior.Player.position;
			CreatureBehavior.Distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.Player.position);

			if (CreatureBehavior.Followdistance >= CreatureBehavior.Distance)
			{
				CreatureBehavior.Agent.isStopped = true;
				CreatureBehavior.CreatureInfoPanel.SetActive(true);
				CreatureBehavior.CreatureInfo.ShowInfo();
			}
			else
			{
				CreatureBehavior.Agent.isStopped = false;
				CreatureBehavior.CreatureInfoPanel.SetActive(false);
			}
			if (CreatureBehavior.Distance >= 15)
			{
				CreatureBehavior.PlayerFound = false;
			}
		}
    }
}