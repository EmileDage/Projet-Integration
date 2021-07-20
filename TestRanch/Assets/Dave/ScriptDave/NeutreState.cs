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

			if(!CreatureBehavior.FoodFound) // Si la creature trouve de la nourriture elle va changer de state et arreter les autres state
            {
				if (!CreatureBehavior.PlayerFound) // Si la creature trouve le joueur elle va changer de state et arreter les autres state
				{
					#region Movement
					if (CreatureBehavior.Targets.Length == 0) return;

					bool search = false;

					// Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
					// if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
					if (CreatureBehavior.Agent.reachedEndOfPath && !CreatureBehavior.Agent.pathPending && float.IsPositiveInfinity(CreatureBehavior.SwitchTime))
					{
						CreatureBehavior.SwitchTime = Time.time + CreatureBehavior.Delay;
					}

					if (Time.time >= CreatureBehavior.SwitchTime)
					{
						CreatureBehavior.Index = UnityEngine.Random.Range(0, CreatureBehavior.Targets.Length);
						search = true;
						CreatureBehavior.SwitchTime = float.PositiveInfinity;
					}

					CreatureBehavior.Index = CreatureBehavior.Index % CreatureBehavior.Targets.Length;
					CreatureBehavior.Agent.destination = CreatureBehavior.Targets[CreatureBehavior.Index].position;

					if (search) CreatureBehavior.Agent.SearchPath();

                    #endregion
                }
                if (CreatureBehavior.PlayerFound)
                {
					if (CreatureBehavior.Happiness >= 30 && CreatureBehavior.Happiness <= 70) // Creature est chill donc en state Neutre
					{
						CreatureBehavior.State = "Neutre";
						CreatureBehavior.Agent.isStopped = true;
						CreatureBehavior.Distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.Player.position);

						if (CreatureBehavior.Followdistance >= CreatureBehavior.Distance)
						{
							CreatureBehavior.CreatureInfoPanel.SetActive(true);
							CreatureBehavior.CreatureInfo.ShowInfo();
						}

						if (CreatureBehavior.Followdistance + 2f <= CreatureBehavior.Distance)
						{
							CreatureBehavior.Agent.isStopped = false;
							CreatureBehavior.CreatureInfoPanel.SetActive(false);
							CreatureBehavior.PlayerFound = false;
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
			
			if(CreatureBehavior.FoodFound) // Creature entre dans le state FoodSearchState et recherche de la nourriture
            {
				CreatureBehavior.SetState(new FoodSearchState(CreatureBehavior));
            }
		}
    }
}
