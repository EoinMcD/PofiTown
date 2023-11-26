using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] public Image image;
    [SerializeField] public Color selectedColor, notSelectedColor;

    private void Awake() {
        DeSelect();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount ==0) {
            InventoryItemUI inventoryItemUI =eventData.pointerDrag.GetComponent<InventoryItemUI>();
            Debug.Log(inventoryItemUI);
            inventoryItemUI.parentAfterDrag=transform;
        }
    }

    public void Select(){
        image.color=selectedColor;
    }

    public void DeSelect(){
        image.color=notSelectedColor;
    }    
}
