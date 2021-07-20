using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCreature : MonoBehaviour
{
    [SerializeField] private FieldCreatureManagement creature;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Enclos enclos;
    [SerializeField] private int slotPos;

    [SerializeField] private Button button;
    [SerializeField] private Sprite sprite;

    public void AddCreatureBtn() // button pour ajouter une creature dans un enclos
    {
        if(button.image.sprite == sprite)
        {
            Debug.Log("no creature found");
            return;
        }
        else
        {
            Debug.Log("add creature");
            creature.creatureInPokeBall[slotPos].IsPokeBall = false; // n<est plus dans le state SLotCapturedState

            creature.creatureInPokeBall[slotPos].transform.position = spawnPos.position; // nouvelle position est dans l'enclos

            creature.creatureInPokeBall[slotPos].Enclos = enclos; // assigne l'enclos a la creature

            enclos.Animaux.Add(creature.creatureInPokeBall[slotPos]); // ajoute la creature dans la liste du script enclos

            creature.creatureInPokeBall[slotPos].IsCaptured = true; // active le state CapturedState

            RemoveFromList();

            button.image.sprite = sprite;
        }
    }

    private void RemoveFromList()
    {
        creature.pokeBallStation.creature.RemoveAt(slotPos); // enleve la creature de la liste du script CreatureCapturedStation
        Debug.Log("removed station");

        RefreshSprite();

        creature.creatureInPokeBall.Clear(); // enleve la creature de la liste du script FieldCreatureManagement
        Debug.Log("removed field");
        
    }

    private void RefreshSprite()
    {
        for(int i = 0; i < creature.CreatureBtn.Length; i++)
        {
            creature.CreatureBtn[i].image.sprite = sprite;
        }
        
    }
}
