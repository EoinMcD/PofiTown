using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : VirtualInventory {
    [SerializeField] GameObject canvases;
    RectTransform inventory;

    [SerializeField]  InventorySlot[] mainInventorySlots;
    [SerializeField]  InventorySlot[] hotbarInventorySlots;

    bool chestOpen = false;
    Chest chestCurrentlyOpen;

    int selectedSlot = -1;

    //Vector3 invNormalPos= new Vector3(485f, 273f, 0f);
    //Vector3 invNormalScale=new Vector3(1f, 1f, 1f);

    //Vector3 invChestPos = new Vector3(200f, 120f, 0f);
    //Vector3 invChestScale = new Vector3(.5f, .5f, .5f);

    private void Awake() {
        isPlayer = true;

        Cursor.visible = false;
        inventoryScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }




    // Start is called before the first frame update
    public override void Start() {
        //base.Start();

        //SetInvUiNormal();
    }
    /*
    public void SetInvUiNormal() {
        inventory.localScale = invNormalScale;
        inventory.position = invNormalPos;
    }
    */

    // Update is called once per frame
    void Update() {
        if (Input.inputString != null) {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number <= 8) {
                ChangeSelectedSlot(number - 1);
            }
        }
        /*if (Input.GetKeyDown(KeyCode.H)) {
            inventory.localScale = invChestScale;
            inventory.position = invChestPos;
        
        }
        */
        if (Input.GetKeyDown(KeyCode.Tab)) {
            OpenInventory();
        }


        /* if (Input.GetKeyDown(KeyCode.T)) {
             foreach (KeyValuePair<SO_Item, int> entry in itemDict) {
                 Debug.Log("Slots Used: " + slotsUsed);
                 Debug.Log(entry.Key.name + " :  " + entry.Value);
             }
         }
         if (Input.GetKeyDown(KeyCode.R)) {
             Debug.Log("Slots Used: " + slotsUsed);
             Debug.Log(itemDict);
         }*/
    }

    public void OpenChestInventory(bool chestOpen, Chest chest) {
        OpenInventory();
        this.chestOpen = chestOpen;
        chestCurrentlyOpen = chest;
    }


    void ChangeSelectedSlot(int newValue) {
        if (selectedSlot >= 0) {
            inventorySlots[selectedSlot].DeSelect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
        InventoryItemUI itemInSlot = inventorySlots[newValue].GetComponentInChildren<InventoryItemUI>();
        if (itemInSlot != null) {
            //playerItem.SpawnItem(itemInSlot.so_Item);
        }
    }

    InventoryItemUI GetSlotInfo() {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();

        if (itemInSlot != null) {
            return itemInSlot;
        }
        else {
            return null;
        }
    }

    public SO_Item GetSelectedItem() {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
        if (itemInSlot != null) {
            return itemInSlot.so_Item;
        }
        else {
            return null;
        }
    }

    public override void ShiftClick(CurrentlyIn currentlyIn, InventoryItemUI item) {
        if (currentlyIn == CurrentlyIn.PlayerHotbar) {
            Debug.Log("dsfndskj" + chestOpen);
            if (chestOpen) {
                //chest
                chestCurrentlyOpen.AddToInventory(item.so_Item, chestCurrentlyOpen.GetInventorySlots(), item.count);
                RemoveItem(item, item.count);
                Debug.Log("TO CHEST");
            }
            else {
                //main
                AddToInventory(item.so_Item, mainInventorySlots, item.count);
                RemoveItem(item, item.count);
            }
        }
        else if (currentlyIn == CurrentlyIn.PlayerMain) {
            if (chestOpen) {
                //chest
                chestCurrentlyOpen.AddToInventory(item.so_Item, chestCurrentlyOpen.GetInventorySlots(), item.count);
                RemoveItem(item, item.count);
            }
            else {
                //hotbar
                AddToInventory(item.so_Item, hotbarInventorySlots, item.count);
                RemoveItem(item, item.count);
            }
        }
        else if (currentlyIn == CurrentlyIn.Chest) {

            AddToInventory(item.so_Item, mainInventorySlots, item.count);
            RemoveItem(item, item.count);
            
        }

    }
}
