using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image image;

    public Transform parentAfterDrag;


    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget=false;
        Debug.Log("being drag");
        parentAfterDrag=transform.parent;
        transform.SetParent(transform.root);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget=true;
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
    }

}
