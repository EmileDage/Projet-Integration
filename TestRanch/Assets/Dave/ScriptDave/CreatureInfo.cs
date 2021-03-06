using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureInfo : MonoBehaviour
{
    public CreatureBehavior creature;

    [SerializeField] private string creatureName;
    [SerializeField] private Text CreatureNameTxtPanel;

    [SerializeField] private Text stateTxtPanel;

    public string hungry;
    [SerializeField] private Text hungryTxtPanel;

    [SerializeField] private string foodLikes;
    [SerializeField] private Text foodLikesTxtPanel;

    public string FoodLikes { get => foodLikes; set => foodLikes = value; }


    public void ShowInfo()
    {
        CreatureNameTxtPanel.text = "Name: " + creatureName;
        stateTxtPanel.text = "State: " + creature.State;
        hungryTxtPanel.text = "Hungry: " + hungry;
        foodLikesTxtPanel.text = "Food Likes: " + FoodLikes;
    }
}
