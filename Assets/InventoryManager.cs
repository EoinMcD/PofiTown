using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] GameObject inventoryItemPrefab;

    int selectedSlot= -1;


    private void Start() {
        ChangeSelectedSlot(0);
    }

    private void Update() {
        if(Input.inputString !=null) {
            bool isNumber = int.TryParse(Input.inputString,out int number);
            if(isNumber && number > 0 && number <= 8) {
                ChangeSelectedSlot(number-1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue){
        if(selectedSlot>=0) {
            inventorySlots[selectedSlot].DeSelect();
        }
        inventorySlots[newValue].Select();
        selectedSlot=newValue;
    }

    public bool AddItem(SO_Item item) {

        for(int i=0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if(itemInSlot!= null && itemInSlot.so_Item == item && itemInSlot.count < item.stackSize && item.stackable) {
                itemInSlot.count++;

                itemInSlot.RefreshCount();
                return true;
            }

        }

        for(int i=0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if(itemInSlot== null) {
                SpawnNewItem(item, slot);
                return true;
            }

        }
        return false;
    }

    void SpawnNewItem(SO_Item item, InventorySlot slot){
        GameObject newItemGo=Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItemUI inventoryItemUI = newItemGo.GetComponent<InventoryItemUI>();
        inventoryItemUI.InitialiseItem(item);
    }

    public SO_Item GetSelectedItem() {
        InventorySlot slot = inventorySlots[selectedSlot];
          InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if(itemInSlot!= null) {
                SO_Item item = itemInSlot.so_Item;
                if(item.useItem){
                    itemInSlot.count--;
                    if(itemInSlot.count<=0){
                        Destroy(itemInSlot.gameObject);
                    }
                    else{
                        itemInSlot.RefreshCount();
                    }
                }

                return item;
            }
            else{
                return null;
            }
    }
}
