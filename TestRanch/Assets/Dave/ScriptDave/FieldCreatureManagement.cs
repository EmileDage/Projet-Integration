using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldCreatureManagement : MonoBehaviour
{
    public CreatureCapturedStation pokeBallStation;
    public List<CreatureBehavior> creatureInPokeBall;
    [SerializeField] private Button[] creature;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(pokeBallStation.creature.Count > creatureInPokeBall.Count)
        {
            for (int i = 0; i < pokeBallStation.creature.Count; i++)
            {
                creatureInPokeBall.Add(pokeBallStation.creature[i]);
                Sprite sprite = pokeBallStation.creature[i].GetComponent<CreatureInfoExtra>().creaturePortrait;
                creature[i].image.sprite = sprite;
            }
        }
    }

}
