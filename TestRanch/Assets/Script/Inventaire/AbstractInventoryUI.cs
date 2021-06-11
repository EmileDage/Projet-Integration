using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUI : MonoBehaviour
{

    public virtual void UpdatePanel()
    {

    }

    public virtual void QuickSendStack(ItemStack stack, DragItem drag)
    {

    }

    public virtual void SetSlotsParent(Slot ItemSlot)
    {
        ItemSlot.ParentUI = this;
    }

    

}
