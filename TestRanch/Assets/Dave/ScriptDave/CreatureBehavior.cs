using Assets.Dave.ScriptDave;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : StateMachine, ICapturable
{
	#region Variable
	public bool playerFound = false;
	public bool foodFound = false;
	[SerializeField] private bool isCaptured = false;
	public Transform player;
	public GameObject creatureInfoPanel;
	public GameObject interactionPanel;
	public float followdistance;
	public float distance;
	public CreatureInfo creatureInfo;

	[SerializeField] private double happiness;

	public string state = null;
	[Header("Food Stuff")]

	public float hungryTimer;
	public Collider targetCollider;
	public GameObject dropRessources;

	private MyTimeManager timeManager;
	private GameManager GM;

	// variable pour le projectile de la creature state Agressif
	[Header("Variable pour la creature Agressive")]
	public Rigidbody projectile;
	public Transform creatureFace;
	public float constant;
	public float NextFire;
	public float FireRate = 1.0f;

	// Variable pour Deplacement de la creature Captured

	[Header("Captured Creature movement")]
	public Transform[] randomTarget;

	[Header("Variable pour le patrol")]
	public Transform[] targets;
	public float delay = 0;
	public int index;

	public IAstarAI agent;
	public float switchTime = float.PositiveInfinity;

	public double Happiness { get => happiness; set => happiness = value; }
	public bool IsCaptured { get => isCaptured; set => isCaptured = value; }

	#endregion

	protected override void Awake()
	{
		base.Awake();
		agent = GetComponent<IAstarAI>();
		timeManager = MyTimeManager.timeInstance;
		//timeManager.GHourPassed += OnGHourPassedHunger;
		//timeManager.GHourPassed += OnGHourPassedHappiness;
		creatureInfoPanel.SetActive(false);
		interactionPanel.SetActive(false);
	}

    private void Start()
    {
		GM = GameManager.gmInstance;
    }

    void Update()
	{
		if(!IsCaptured)
        {
			SetState(new NeutreState(this));
		}
		else
        {
			SetState(new CapturedState(this));
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerFound = true;
		}
		if(other.gameObject.tag == "Food" && creatureInfo.hungry == "Yes")
        {
			targetCollider = other;
			foodFound = true;
		}		
	}

	private void OnGHourPassedHunger(object source)
    {
		if(creatureInfo.hungry == "No")
        {
			hungryTimer--;
			if (hungryTimer == 0)
			{
				creatureInfo.hungry = "Yes";
				hungryTimer = 15;
			}
		}
    }

	private void OnGHourPassedHappiness(object source)
	{
		if (creatureInfo.hungry == "Yes" && isCaptured)
		{
			if (happiness == 0)
			{
				
			}
			else
            {
				happiness--;
			}
		}
	}

	public void Shoot()
	{
		// spawn projectile
		if(Time.time > NextFire)
        {
			Rigidbody clone;
			clone = (Rigidbody)Instantiate(projectile, creatureFace.position, creatureFace.rotation);
			clone.velocity = creatureFace.TransformDirection(Vector3.forward * 10);
			NextFire = Time.time + FireRate;			
		}
		
	}

	public double GetHappiness()
    {
		return Happiness;
    }

	public void ModifyHappiness(double modifier)
    {
		Happiness = Happiness * (1 + modifier);
    }

    public void Capture()
    {
        //GM.Joueur.
    }

	public void DropRessource()
	{
		if (state == "Pacifique")
		{
			Instantiate(dropRessources, transform.position, transform.rotation);
		}
	}
}
