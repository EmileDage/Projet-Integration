using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Dave.ScriptDave
{
    public class NeutreState : State
    {
        public NeutreState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
        {
        }

        public override void Neutre()
        {
			if(!CreatureBehavior.foodFound) // Si la creature trouve de la nourriture elle va changer de state et arreter les autres state
            {
				if (!CreatureBehavior.playerFound) // Si la creature trouve le joueur elle va changer de state et arreter les autres state
				{
					if (CreatureBehavior.targets.Length == 0) return;

					bool search = false;

					// Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
					// if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
					if (CreatureBehavior.agent.reachedEndOfPath && !CreatureBehavior.agent.pathPending && float.IsPositiveInfinity(CreatureBehavior.switchTime))
					{
						CreatureBehavior.switchTime = Time.time + CreatureBehavior.delay;
					}

					if (Time.time >= CreatureBehavior.switchTime)
					{
						CreatureBehavior.index = UnityEngine.Random.Range(0, CreatureBehavior.targets.Length);
						search = true;
						CreatureBehavior.switchTime = float.PositiveInfinity;
					}

					CreatureBehavior.index = CreatureBehavior.index % CreatureBehavior.targets.Length;
					CreatureBehavior.agent.destination = CreatureBehavior.targets[CreatureBehavior.index].position;

					if (search) CreatureBehavior.agent.SearchPath();
				}
				if(CreatureBehavior.playerFound)
                {
					if (CreatureBehavior.Happiness >= 30 && CreatureBehavior.Happiness <= 70) // Creature est chill donc en state Neutre
					{
						CreatureBehavior.state = "Neutre";
						CreatureBehavior.agent.isStopped = true;
						CreatureBehavior.distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.player.position);

						if (CreatureBehavior.followdistance >= CreatureBehavior.distance)
						{
							CreatureBehavior.creatureInfoPanel.SetActive(true);
						}

						if (CreatureBehavior.followdistance + 2f <= CreatureBehavior.distance)
						{
							CreatureBehavior.agent.isStopped = false;
							CreatureBehavior.creatureInfoPanel.SetActive(false);
							CreatureBehavior.playerFound = false;
						}
					}

					if (CreatureBehavior.Happiness <= 0) // Creature est mad donc en state Agressif
					{
						CreatureBehavior.SetState(new AgressifState(CreatureBehavior));
					}

					if (CreatureBehavior.Happiness > 0 && CreatureBehavior.Happiness < 30) // Creature est sad donc en state Afraid
					{
						CreatureBehavior.SetState(new AfraidState(CreatureBehavior));
					}

					if (CreatureBehavior.Happiness > 70) // Creature est happy donc en state Pacifique
					{
						CreatureBehavior.SetState(new PacifiqueState(CreatureBehavior));
					}
				}
				
			}
			
			if(CreatureBehavior.foodFound) // Creature entre dans le state FoodSearchState et recherche de la nourriture
            {
				CreatureBehavior.SetState(new FoodSearchState(CreatureBehavior));
            }
		}
    }
}
