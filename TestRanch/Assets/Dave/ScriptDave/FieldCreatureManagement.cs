using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldCreatureManagement : MonoBehaviour
{
    public CreatureCapturedStation pokeBallStation;
    public List<CreatureBehavior> creatureInPokeBall;
    [SerializeField] private Button[] creatureBtn;

    public Button[] CreatureBtn { get => creatureBtn; set => creatureBtn = value; }

    void Update()
    {
        if(pokeBallStation.creature.Count > creatureInPokeBall.Count)
        {
            for (int i = 0; i < pokeBallStation.creature.Count; i++)
            {
                if(creatureInPokeBall.Contains(pokeBallStation.creature[i]))
                {
                    Debug.Log("already in list");
                }
                else
                {
                    creatureInPokeBall.Add(pokeBallStation.creature[i]);
                    Sprite sprite = pokeBallStation.creature[i].GetComponent<CreatureInfoExtra>().creaturePortrait;
                    CreatureBtn[i].image.sprite = sprite;
                }
            }
        }
    }
}
