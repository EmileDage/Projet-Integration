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
        #region Movement

        if (CreatureBehavior.randomTarget.Length == 0) return;

		bool search = false;

		if (CreatureBehavior.agent.reachedEndOfPath && !CreatureBehavior.agent.pathPending && float.IsPositiveInfinity(CreatureBehavior.switchTime))
		{
			CreatureBehavior.switchTime = Time.time + CreatureBehavior.delay;
		}

		if (Time.time >= CreatureBehavior.switchTime)
		{
			CreatureBehavior.index = UnityEngine.Random.Range(0, CreatureBehavior.randomTarget.Length);
			search = true;
			CreatureBehavior.switchTime = float.PositiveInfinity;
		}

		CreatureBehavior.index = CreatureBehavior.index % CreatureBehavior.randomTarget.Length;
		CreatureBehavior.agent.destination = CreatureBehavior.randomTarget[CreatureBehavior.index].position;

		if (search) CreatureBehavior.agent.SearchPath();

        #endregion

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

	}
}