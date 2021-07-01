using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCreatureInfo : MonoBehaviour
{
    [SerializeField] private FieldCreatureManagement creature;
    [SerializeField] private int slotPos;

    [SerializeField] private Button button;
    [SerializeField] private Sprite sprite;

    public void ShowInfo() // button pour ajouter une creature dans un enclos
    {
        if (button.image.sprite == sprite)
        {
            Debug.Log("no creature found");
            return;
        }
        else
        {
            Debug.Log("Show Info");
            creature.creatureInPokeBall[slotPos].creatureInfoPanelExtra.SetActive(true); // n<est plus dans le state SLotCapturedState
        }
    }
}
