using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureInfoExtra : MonoBehaviour
{
    [SerializeField] private CreatureBehavior creature;
    [SerializeField] private Projectile projectile;
    [SerializeField] private AIPath speed;

    [SerializeField] private string creatureName;
    [SerializeField] private Text CreatureNameTxtPanel;

    [SerializeField] private Text stateTxtPanel;

    [SerializeField] private string foodLikes;
    [SerializeField] private Text foodLikesTxtPanel;

    public Sprite creaturePortrait;
    [SerializeField] private Image creaturePortraitImage;

    [SerializeField] private Text creatureHappinessTxtPanel;

    [SerializeField] private Text creatureAttackPowerTxtPanel;

    [SerializeField] private Text creatureSpeedTxtPanel;

    [SerializeField] private string creatureBiome;
    [SerializeField] private Text creatureBiomeTxtPanel;

    [SerializeField] private Text creatureDropRessourceTxtPanel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (creature.Agent.isStopped)
        {
            CreatureNameTxtPanel.text = "Name: " + creatureName;
            stateTxtPanel.text = "State: " + creature.State1;
            foodLikesTxtPanel.text = "Food Likes: " + foodLikes;
            creaturePortraitImage.sprite = creaturePortrait;
            creatureHappinessTxtPanel.text = "Happiness: " + creature.Happiness + " / 100";
            creatureAttackPowerTxtPanel.text = "AttackPower: " + projectile.Attack;
            creatureSpeedTxtPanel.text = "Speed: " + speed.maxSpeed;
            creatureBiomeTxtPanel.text = "Biome: " + creatureBiome;
            creatureDropRessourceTxtPanel.text = "Ressource Drop: " + creature.DropRessources.name;

        }

    }
}
