using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;
        DragItem drag = dragged.GetComponent<DragItem>();
        if (dragged != null)
        {
            drag.ParentSlot.RemoveItem();
        }
        drag.ParentSlot.UpdateSlot();
    }
}
