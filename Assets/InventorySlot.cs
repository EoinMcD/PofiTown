using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("hello");
        if(transform.childCount ==0) {
            InventoryItemUI inventoryItemUI =eventData.pointerDrag.GetComponent<InventoryItemUI>();
            Debug.Log(inventoryItemUI);
            inventoryItemUI.parentAfterDrag=transform;
        }
    }

    
}
