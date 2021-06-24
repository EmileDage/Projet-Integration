using Assets.Dave.ScriptDave;
using UnityEngine;

internal class CapturedState : State
{
    public CapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
	{
		CreatureBehavior.agent.canMove = true;
		CreatureBehavior.agent.canSearch = true;
		CreatureBehavior.IsCaptured = true;
		CreatureBehavior.state = "Captured";
		CreatureBehavior.distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.player.position);

		// Comportement d'interaction avec l'enclos a cath

		//Faire l'assignation des animaux a lenclos variable Animaux d'enclos

		#region AssignationEnclos
		if(CreatureBehavior.randomTarget.Count == 0)
        {
			for (int i = 0; i < CreatureBehavior.enclos.PatrolPoints.Length; i++)
			{
				CreatureBehavior.randomTarget.Add(CreatureBehavior.enclos.PatrolPoints[i]);
			}
		}


		#endregion

		#region Movement

		if (CreatureBehavior.randomTarget.Count == 0) return;

		bool search = false;

		if (CreatureBehavior.agent.reachedEndOfPath && !CreatureBehavior.agent.pathPending && float.IsPositiveInfinity(CreatureBehavior.switchTime))
		{
			CreatureBehavior.switchTime = Time.time + CreatureBehavior.delay;
		}

		if (Time.time >= CreatureBehavior.switchTime)
		{
			CreatureBehavior.index = UnityEngine.Random.Range(0, CreatureBehavior.randomTarget.Count);
			search = true;
			CreatureBehavior.switchTime = float.PositiveInfinity;
		}

		CreatureBehavior.index = CreatureBehavior.index % CreatureBehavior.randomTarget.Count;
		CreatureBehavior.agent.destination = CreatureBehavior.randomTarget[CreatureBehavior.index].position;

		if (search) CreatureBehavior.agent.SearchPath();

        #endregion

        #region InteractionInfo
        if (CreatureBehavior.playerFound)
		{
			if (CreatureBehavior.distance <= 5)
			{
				if (!CreatureBehavior.creatureInfoPanel.activeInHierarchy)
				{
					CreatureBehavior.interactionPanel.SetActive(true);
				}
				if (Input.GetButtonDown("Interact"))
				{
					CreatureBehavior.agent.isStopped = true;
					CreatureBehavior.interactionPanel.SetActive(false);
					CreatureBehavior.creatureInfoPanelExtra.SetActive(true);
				}
			}
			if (CreatureBehavior.distance > 8)
			{
				CreatureBehavior.playerFound = false;
				CreatureBehavior.agent.isStopped = false;
				CreatureBehavior.interactionPanel.SetActive(false);
				CreatureBehavior.creatureInfoPanelExtra.SetActive(false);
			}
		}
		#endregion
	}
}