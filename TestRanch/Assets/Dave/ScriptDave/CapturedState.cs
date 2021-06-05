using Assets.Dave.ScriptDave;
using UnityEngine;

internal class CapturedState : State
{
    public CapturedState(CreatureBehavior creatureBehavior) : base(creatureBehavior)
	{
		CreatureBehavior.IsCaptured = true;
		CreatureBehavior.state = "Captured";
		CreatureBehavior.distance = Vector3.Distance(CreatureBehavior.transform.position, CreatureBehavior.player.position);
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
				CreatureBehavior.creatureInfoPanel.SetActive(true);
			}
		}
		if (CreatureBehavior.distance > 5)
		{
			CreatureBehavior.agent.isStopped = false;
			CreatureBehavior.interactionPanel.SetActive(false);
			CreatureBehavior.creatureInfoPanel.SetActive(false);
		}

		// Comportement d'interaction avec l'enclos a cath
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

		// Drop de Ressources selon le happiness
		/*
		 * if(CreatureBehavior.happiness <= 0) {
		 * 
		 * }
		 * 
		 * if(CreatureBehavior.happiness > 0 && CreatureBehavior.happiness < 30) {
		 *		CreatureBehavior.DropRessource();
		 * }
		 * 
		 * if(CreatureBehavior.happiness >= 30 && CreatureBehavior.happiness < 60) {
		 *		for(int i =0; i < 2; i++) {
		 *		CreatureBehavior.DropRessource();
		 *		}
		 * }
		 * 
		 * if(CreatureBehavior.happiness >= 60 && CreatureBehavior.happiness < 90) {
		 *		for(int i =0; i < 3; i++) {
		 *		CreatureBehavior.DropRessource();
		 *		}
		 * }
		 * 
		 * if(CreatureBehavior.happiness >= 90) {
		 *		for(int i =0; i < 4; i++) {
		 *		CreatureBehavior.DropRessource();
		 *		}
		 * }
		 * */

	}
}