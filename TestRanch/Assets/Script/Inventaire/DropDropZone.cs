using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDropZone : MonoBehaviour, IDropHandler
{
    GameManager GM;

    private void Start()
    {
        GM = GameManager.gmInstance;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;
        DragItem drag = dragged.GetComponent<DragItem>();
        if (dragged != null)
        {
            drag.ParentSlot.ItemStack.InstantiateRessourceObject(GM.Joueur.Offset);
            drag.ParentSlot.RemoveItem();
        }
        drag.ParentSlot.UpdateSlot();
    }
}
