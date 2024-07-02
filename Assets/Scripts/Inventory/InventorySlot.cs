using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] public Image image;
    [SerializeField] public Color selectedColor, notSelectedColor;

    [SerializeField] GameObject inventoryItemPrefab;

    SO_Item itemInSlot;

    private void Awake() {
        DeSelect();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount ==0) {
            InventoryItemUI inventoryItemUI =eventData.pointerDrag.GetComponent<InventoryItemUI>();
            Debug.Log(inventoryItemUI);
            inventoryItemUI.parentAfterDrag.GetComponent<InventorySlot>().ResetItem();
            inventoryItemUI.parentAfterDrag=transform;
            itemInSlot = inventoryItemUI.so_Item;
        }
    }

    public void Select(){
        image.color=selectedColor;
    }

    public void DeSelect(){
        image.color=notSelectedColor;
    }    

    public void ResetItem() {
        itemInSlot = null;
    }

    public SO_Item GetItemInSlot() {
        return itemInSlot;
    }

    public void AddItemInSlot(SO_Item item) {
        itemInSlot = item ;
    }

    public void AddItem(SO_Item item) {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, transform);
        InventoryItemUI inventoryItemUI = newItemGo.GetComponent<InventoryItemUI>();
        inventoryItemUI.InitialiseItem(item);
        AddItemInSlot(item);
    }
}
