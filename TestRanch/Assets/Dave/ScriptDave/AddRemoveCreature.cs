using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemoveCreature : MonoBehaviour
{
    [SerializeField] private FieldCreatureManagement creature;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Enclos enclos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCreature()
    {
        creature.creatureInPokeBall[0].IsPokeBall = false;
        creature.creatureInPokeBall[0].transform.position = spawnPos.position;
        creature.creatureInPokeBall[0].enclos = enclos;
        creature.creatureInPokeBall[0].IsCaptured = true;       
    }
}
