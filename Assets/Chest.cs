using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Chest : VirtualInventory, I_Interactable {
    PlayerInventory player;
    bool chestOpen = false;

    private void Awake() {
        isPlayer = false;
    }

    public void Interact(PlayerInteract player) {
        this.player = player.GetComponent<PlayerInventory>();
        OpenInventory();
    }

    public override void OpenInventory() {
        chestOpen = !chestOpen;
        base.OpenInventory();
        player.OpenChestInventory(chestOpen, this);
        Cursor.visible = true;
        //inventoryScreen.SetActive(!inventoryScreen.activeInHierarchy);

        Cursor.lockState = CursorLockMode.None;
    }

    public override void ShiftClick(CurrentlyIn currentlyIn, InventoryItemUI item) {
        Debug.Log("THROUGH CHEST");
        player.ShiftClick(currentlyIn, item);
    }


}
