using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class VirtualInventory : MonoBehaviour {
    [SerializeField] protected GameObject inventoryScreen;

    [SerializeField] protected InventorySlot[] inventorySlots;
    [SerializeField] protected GameObject inventoryItemPrefab;

    protected bool isPlayer;


    public virtual void Start() {
        Cursor.visible = false;
        inventoryScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public virtual void ShiftClick(CurrentlyIn currentlyIn, InventoryItemUI item) {
    }

    public InventorySlot[] GetInventorySlots() {
        return inventorySlots;
    }

    public virtual void OpenInventory() {
        Cursor.visible = !Cursor.visible;
        inventoryScreen.SetActive(!inventoryScreen.activeInHierarchy);

        if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
        }
        else { Cursor.lockState = CursorLockMode.Locked; }
    }



    public bool AddToInventory(SO_Item item, InventorySlot[] inventorySlots, int count = 1) {
        Debug.Log("ADDING " + count);
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();

            if (itemInSlot != null && itemInSlot.so_Item == item && itemInSlot.count < item.stackSize) {
                while (count > 0) {

                    if (itemInSlot.count > item.stackSize - 1) {
                        AddToInventory(item, inventorySlots, count);
                        count--;
                        itemInSlot.RefreshCount();
                    }
                    else {
                        count--;
                        itemInSlot.count++;
                        itemInSlot.RefreshCount();
                    }

                }
                return true;
            }

        }

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                if (count > 1) {
                    count--;
                    AddToInventory(item, inventorySlots, count);
                }
                return true;

            }


        }
        return false;
    }

    public void RemoveItem(InventoryItemUI item, int count = 1) {
        item.count -= count; ;
        item.RefreshCount();
    }

    protected void SpawnNewItem(SO_Item item, InventorySlot slot) {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItemUI inventoryItemUI = newItemGo.GetComponent<InventoryItemUI>();
        inventoryItemUI.InitialiseItem(item);
    }

    public bool AddItem(SO_Item item) {

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if (itemInSlot != null && itemInSlot.so_Item == item && itemInSlot.count < item.stackSize && item.stackable) {
                itemInSlot.count++;

                itemInSlot.RefreshCount();
                return true;
            }

        }

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);

                return true;
            }

        }
        return false;
    }

    







}
