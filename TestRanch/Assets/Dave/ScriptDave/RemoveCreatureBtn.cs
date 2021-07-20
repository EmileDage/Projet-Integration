using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCreatureBtn : MonoBehaviour
{
    [SerializeField] private RemoveCreatureManagement removeCreature;
    [SerializeField] private Enclos enclos;
    [SerializeField] private int slotPos;

    [SerializeField] private Button button;
    [SerializeField] private Sprite sprite;

    public void RemoveCreature()
    {
        if (button.image.sprite == sprite)
        {
            Debug.Log("no creature found");
            Debug.Log("removeButton");
            return;
        }
        else
        {
            Debug.Log("remove creature");
            removeCreature.CreatureInEnclos[slotPos].IsCaptured = false;

            removeCreature.CreatureInEnclos[slotPos].RandomTarget = null;

            removeCreature.CreatureInEnclos[slotPos].transform.position = enclos.Animaux[slotPos].SpawnPoint.position;

            Debug.Log("Works");

            enclos.Animaux.RemoveAt(slotPos);

            removeCreature.CreatureInEnclos[slotPos].Enclos = null;

            removeCreature.CreatureInEnclos[slotPos].OnlyOnce = false;

            Debug.Log("Works 2");

            removeCreature.CreatureInEnclos.RemoveAt(slotPos);

            Debug.Log("Works 3");

            button.image.sprite = sprite;

        }
    }
}
