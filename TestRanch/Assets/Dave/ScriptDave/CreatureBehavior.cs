using Assets.Dave.ScriptDave;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : StateMachine, ICapturable
{
	#region Variable

	[Header("bool Variable")]
	[SerializeField] private bool playerFound = false;
	[SerializeField] private bool foodFound = false;
	[SerializeField] private bool isCaptured = false;
	[SerializeField] private bool isPokeBall = false;
	[SerializeField] private bool onlyOnce = false;

	[SerializeField] private Transform player;
	[SerializeField] private GameObject creatureInfoPanel;
	[SerializeField] private GameObject creatureInfoPanelExtra;
	[SerializeField] private float followdistance;
	[SerializeField] private float distance;
	[SerializeField] private CreatureInfo creatureInfo;
	[SerializeField] private CreatureInfoExtra creatureInfoExtra;

	[SerializeField] private double happiness;

	[SerializeField] private string state = null;

	[Header("Food Stuff")]
	[SerializeField] private float hungryTimer;
	[SerializeField] private float hungryTimerCooldown;
	[SerializeField] private Collider targetCollider;
	[SerializeField] private Materiaux dropRessources;
	[SerializeField] private Transform dropPos;

	[Header("Time Stuff")]
	private MyTimeManager timeManager;
	[SerializeField] private int cooldownDropRessource = 10;
	[SerializeField] private int maxCooldownDropRessource = 10;


	private GameManager GM;

	// variable pour le projectile de la creature state Agressif
	[Header("Variable pour la creature Agressive")]
	[SerializeField] private GameObject projectile;
	[SerializeField] private Transform creatureFace;
	[SerializeField] private float constant;
	[SerializeField] private float NextFire;
	[SerializeField] private float FireRate = 1.0f;

	// Variable pour Deplacement de la creature Captured

	[Header("Captured Creature")]
	[SerializeField] private List <Transform> randomTarget;
	[SerializeField] private Transform pokeballTransform;
	[SerializeField] private Enclos enclos;
	[SerializeField] private CreatureCapturedStation listCreaturePokeBall;
	[SerializeField] private GameObject PeacefulMoodMes;
	[SerializeField] private GameObject TooManyCreatureMes;

	[Header("Variable pour le patrol")]
	[SerializeField] private Transform[] targets;
	[SerializeField] private float delay = 0;
	[SerializeField] private int index;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private IAstarAI agent;
	[SerializeField] private float switchTime = float.PositiveInfinity;

    #region public ref
    public double Happiness { get => happiness; set => happiness = value; }
	public bool IsCaptured { get => isCaptured; set => isCaptured = value; }
    public bool IsPokeBall { get => isPokeBall; set => isPokeBall = value; }
    public List<Transform> RandomTarget { get => randomTarget; set => randomTarget = value; }
    public Transform PokeballTransform { get => pokeballTransform; set => pokeballTransform = value; }
    public Enclos Enclos { get => enclos; set => enclos = value; }
    public CreatureCapturedStation ListCreaturePokeBall { get => listCreaturePokeBall; set => listCreaturePokeBall = value; }
    public GameObject PeacefulMoodMes1 { get => PeacefulMoodMes; set => PeacefulMoodMes = value; }
    public GameObject TooManyCreatureMes1 { get => TooManyCreatureMes; set => TooManyCreatureMes = value; }
    public Transform[] Targets { get => targets; set => targets = value; }
    public Transform SpawnPoint { get => spawnPoint; set => spawnPoint = value; }
    public IAstarAI Agent { get => agent; set => agent = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }
    public Transform CreatureFace { get => creatureFace; set => creatureFace = value; }
    public Collider TargetCollider { get => targetCollider; set => targetCollider = value; }
    public string State { get => state; set => state = value; }
    public CreatureInfo CreatureInfo { get => creatureInfo; set => creatureInfo = value; }
    public float Distance { get => distance; set => distance = value; }
    public float Followdistance { get => followdistance; set => followdistance = value; }
    public GameObject CreatureInfoPanelExtra { get => creatureInfoPanelExtra; set => creatureInfoPanelExtra = value; }
    public GameObject CreatureInfoPanel { get => creatureInfoPanel; set => creatureInfoPanel = value; }
    public Transform Player { get => player; set => player = value; }
    public bool OnlyOnce { get => onlyOnce; set => onlyOnce = value; }
    public bool FoodFound { get => foodFound; set => foodFound = value; }
    public bool PlayerFound { get => playerFound; set => playerFound = value; }
    public float SwitchTime { get => switchTime; set => switchTime = value; }
    public int Index { get => index; set => index = value; }
    public float Delay { get => delay; set => delay = value; }
    public Materiaux DropRessources { get => dropRessources; set => dropRessources = value; }
    public CreatureInfoExtra CreatureInfoExtra { get => creatureInfoExtra; set => creatureInfoExtra = value; }

    #endregion

    #endregion

    protected override void Awake()
	{
		base.Awake();
		Agent = GetComponent<IAstarAI>();
	}


    private void Start()
    {
		GM = GameManager.gmInstance;
		timeManager = MyTimeManager.timeInstance;
		timeManager.GHourPassed += OnGHourPassed;
		CreatureInfoPanel.SetActive(false);
		CreatureInfoPanelExtra.SetActive(false);
	}

    void Update()
	{
		if(!IsCaptured)
        {
			if (!IsPokeBall)
				SetState(new NeutreState(this));
			else
				SetState(new SlotCapturedState(this));
		}
		else
        {
			if(IsPokeBall)
            {

				SetState(new SlotCapturedState(this));
			}
			else
			{
				SetState(new CapturedState(this));
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerFound = true;
		}
		if(other.gameObject.tag == "produit" && CreatureInfo.hungry == "Yes")
        {
			WorldObjectMateriaux food = other.GetComponent<WorldObjectMateriaux>();
			Debug.Log(food.Item().Nom);
			Debug.Log(CreatureInfo.FoodLikes);
			if(Fonctions.produits_vegetaux.Equals(food.Item().Funct) && food.Item().Nom.Equals(CreatureInfo.FoodLikes))
            {
				TargetCollider = other;
				FoodFound = true;
			}
		}		
	}

	private void OnGHourPassed(object source)
    {
		if(!IsPokeBall)
        {
			cooldownDropRessource--;
			if (cooldownDropRessource == 0)
			{
				cooldownDropRessource = maxCooldownDropRessource;
                if ( isCaptured && Enclos.Boosted_grass)
                {
					RessourceDrop();
					RessourceDrop();
					Debug.Log("ressource double");
				}
				else
                {
					RessourceDrop();
				}
			}
			if(!isCaptured)
			HourPassedHunger();
		}
    }

	private void HourPassedHunger()
    {
		if(CreatureInfo.hungry == "No")
        {
			hungryTimer--;
			if (hungryTimer == 0)
			{
				CreatureInfo.hungry = "Yes";
				hungryTimer = hungryTimerCooldown;
			}
		}
    }

	private void RessourceDrop()
	{
		if (isCaptured)
		{			
			if(happiness <= 0) {
		  
		  }
		  
		  if(happiness > 0 && happiness < 30) {
				DropRessourceAnimal();
		  }
		  
		  if(happiness >= 30 && happiness < 60) {
		 		for(int i =0; i < 2; i++) {
					DropRessourceAnimal();
		 		}
		  }
		  
		  if(happiness >= 60 && happiness < 90) {
		 		for(int i =0; i < 3; i++) {
		 			DropRessourceAnimal();
		 		}
		  }
		  
		  if(happiness >= 90) {
		 		for(int i =0; i < 4; i++) {
		 			DropRessourceAnimal();
		 		}
		  }
		}
	}

	public void Shoot()
	{
		// spawn projectile
		if(Time.time > NextFire)
        {
			var relativePos = Player.position - transform.position;
			var rotation = Quaternion.LookRotation(relativePos);
			relativePos.x = 0f;
			relativePos.z = 0f;
			transform.rotation = rotation;
			Instantiate(Projectile, CreatureFace.position, CreatureFace.rotation);			
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

		if(State == "Pacifique")
        {
			if(ListCreaturePokeBall.creature.Count == 4)
            {
				TooManyCreatureMes1.GetComponent<OnScreenMessage>().StartCounter("You can't have more then 4 creatures captured, you should place them in an animal pen");
            }
			else
            {
				IsPokeBall = true;
			}
		}
		else
        {
			PeacefulMoodMes1.GetComponent<OnScreenMessage>().StartCounter("The Creature must be in the mood Peaceful to be captured");
        }

	}

	public void DropRessourceAnimal()
	{
		DropRessources.SpawnAsObject(new ItemStack(DropRessources, 1), dropPos);
	}
}
