using Assets.Dave.ScriptDave;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : StateMachine, ICapturable
{
	#region Variable

	[Header("bool Variable")]
	public bool playerFound = false;
	public bool foodFound = false;
	[SerializeField] private bool isCaptured = false;
	[SerializeField] private bool isPokeBall = false;
	public bool onlyOnce = false;

	public Transform player;
	public GameObject creatureInfoPanel;
	public GameObject creatureInfoPanelExtra;
	public GameObject interactionPanel;
	public float followdistance;
	public float distance;
	public CreatureInfo creatureInfo;

	[SerializeField] private double happiness;

	public string state = null;

	[Header("Food Stuff")]
	public float hungryTimer;
	public Collider targetCollider;
	public Materiaux dropRessources;

	[Header("Time Stuff")]
	private MyTimeManager timeManager;
	[SerializeField] private int cooldownDropRessource = 10;
	[SerializeField] private int maxCooldownDropRessource = 10;


	private GameManager GM;

	// variable pour le projectile de la creature state Agressif
	[Header("Variable pour la creature Agressive")]
	public Rigidbody projectile;
	public Transform creatureFace;
	public float constant;
	public float NextFire;
	public float FireRate = 1.0f;

	// Variable pour Deplacement de la creature Captured

	[Header("Captured Creature")]
	public List <Transform> randomTarget;
	public Transform pokeballTransform;
	public Enclos enclos;
	public CreatureCapturedStation listCreaturePokeBall;

	[Header("Variable pour le patrol")]
	public Transform[] targets;
	public float delay = 0;
	public int index;
	public Transform spawnPoint;

	public IAstarAI agent;
	public float switchTime = float.PositiveInfinity;

	public double Happiness { get => happiness; set => happiness = value; }
	public bool IsCaptured { get => isCaptured; set => isCaptured = value; }
    public bool IsPokeBall { get => isPokeBall; set => isPokeBall = value; }

    #endregion

    protected override void Awake()
	{
		base.Awake();
		agent = GetComponent<IAstarAI>();
	}


    private void Start()
    {
		GM = GameManager.gmInstance;
		timeManager = MyTimeManager.timeInstance;
		timeManager.GHourPassed += OnGHourPassed;
		creatureInfoPanel.SetActive(false);
		interactionPanel.SetActive(false);
		creatureInfoPanelExtra.SetActive(false);
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
			playerFound = true;
		}
		if(other.gameObject.tag == "produit" && creatureInfo.hungry == "Yes")
        {
			WorldObjectMateriaux food = other.GetComponent<WorldObjectMateriaux>();
			if(Fonctions.produits_vegetaux.Equals(food.Item().Funct))
            {
				targetCollider = other;
				foodFound = true;
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
                if ( isCaptured && enclos.Boosted_grass)
                {
					RessourceDrop();
					RessourceDrop();
					Debug.Log("ressourcve double");
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
		Debug.Log("iscapturing");
		//GM.Joueur.
		if(state == "Pacifique")
        {
			if(listCreaturePokeBall.creature.Count == 4)
            {
				Debug.Log("show message qu'il y deja trop de creature capturer pour l'instant, il est suggerer d'aller les placer dans un enclos ou de les relacher dans la nature a partir du menu creature");
            }
			else
            {
				IsPokeBall = true;
				SetState(new SlotCapturedState(this));
			}
		}
		else
        {
			Debug.Log("show message que la creature doit etre dans le state pacifique pour etre capturer");
        }

	}

	public void DropRessourceAnimal()
	{
		dropRessources.SpawnAsObject(new ItemStack(dropRessources, 1), transform);
	}
}
