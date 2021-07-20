using Assets.Dave.ScriptDave;
using UnityEngine;

internal class CapturedState : State
{
    public CapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
	{
		CreatureBehavior.Agent.canMove = true;
		CreatureBehavior.Agent.isStopped = false;
		CreatureBehavior.Agent.canSearch = true;
		CreatureBehavior.IsCaptured = true;
		CreatureBehavior.State = "Captured";
		CreatureBehavior.Distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.Player.position);

		// Comportement d'interaction avec l'enclos a cath

		//Faire l'assignation des animaux a lenclos variable Animaux d'enclos

		#region AssignationEnclos
		if(CreatureBehavior.RandomTarget.Count == 0)
        {
			for (int i = 0; i < CreatureBehavior.Enclos.PatrolPoints.Length; i++)
			{
				CreatureBehavior.RandomTarget.Add(CreatureBehavior.Enclos.PatrolPoints[i]);
			}
		}
		


		#endregion

		#region Movement

		if (CreatureBehavior.RandomTarget.Count == 0) return;

		bool search = false;

		if (CreatureBehavior.Agent.reachedEndOfPath && !CreatureBehavior.Agent.pathPending && float.IsPositiveInfinity(CreatureBehavior.SwitchTime))
		{
			CreatureBehavior.SwitchTime = Time.time + CreatureBehavior.Delay;
		}

		if (Time.time >= CreatureBehavior.SwitchTime)
		{
			CreatureBehavior.Index = UnityEngine.Random.Range(0, CreatureBehavior.RandomTarget.Count);
			search = true;
			CreatureBehavior.SwitchTime = float.PositiveInfinity;
		}

		CreatureBehavior.Index = CreatureBehavior.Index % CreatureBehavior.RandomTarget.Count;
		CreatureBehavior.Agent.destination = CreatureBehavior.RandomTarget[CreatureBehavior.Index].position;

		if (search) CreatureBehavior.Agent.SearchPath();

        #endregion

	}
}