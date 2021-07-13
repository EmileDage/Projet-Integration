using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private Slot parentSlot;
    private RectTransform recT;
    private Vector3 initPos;
    private CanvasGroup cGroup;

    public Slot ParentSlot { get => parentSlot; set => parentSlot = value; }
    public Canvas Canvas { get => canvas; set => canvas = value; }

    private void Awake()
    {
        recT = GetComponent<RectTransform>();
        initPos = recT.anchoredPosition;
        cGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("OnBeginDrag");
        recT.SetParent(Canvas.GetComponent<RectTransform>(), true);
        cGroup.blocksRaycasts=false;
        cGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log("dragging");
        recT.anchoredPosition += eventData.delta/Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();
        UIManager.Instance.ItemSound();
      //  Debug.Log("OnEndDrag");
    }

    public void ResetPosition()
    {
        //Debug.Log(parentSlot);
        recT.SetParent(parentSlot.gameObject.transform);
        recT.anchoredPosition = initPos;
        cGroup.blocksRaycasts = true;
        cGroup.alpha = 1;
       // ParentSlot.UpdateSlot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.ItemSound();
        if (Input.GetButton("QuickAdd"))
        {
           // Debug.Log("shift click");
            parentSlot.QuickTransfer(this);
        }
    }
}
