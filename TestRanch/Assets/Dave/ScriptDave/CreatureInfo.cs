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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(creature.agent.isStopped)
        {
            CreatureNameTxtPanel.text = "Name: " + creatureName;
            stateTxtPanel.text = "State: " + creature.state;
            hungryTxtPanel.text = "Hungry: " + hungry;
            foodLikesTxtPanel.text = "Food Likes: " + foodLikes;
        }

    }
}
