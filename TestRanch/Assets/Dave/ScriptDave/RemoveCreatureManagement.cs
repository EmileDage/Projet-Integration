using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCreatureManagement : MonoBehaviour
{
    [SerializeField] private Enclos enclos;
    [SerializeField] private List<CreatureBehavior> creatureInEnclos;
    [SerializeField] private Button[] creatureBtn;

    public List<CreatureBehavior> CreatureInEnclos { get => creatureInEnclos; set => creatureInEnclos = value; }

    void Update()
    {
        if(enclos.Animaux.Count > CreatureInEnclos.Count)
        {
            for(int i = 0; i < enclos.Animaux.Count; i ++)
            {
                if (CreatureInEnclos.Contains(enclos.Animaux[i]))
                {
                    Debug.Log("already in list");
                }
                else
                {
                    CreatureInEnclos.Add(enclos.Animaux[i]);
                    Sprite sprite = enclos.Animaux[i].GetComponent<CreatureInfoExtra>().creaturePortrait;
                    creatureBtn[i].image.sprite = sprite;
                }
            }
        }
    }
}
