using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Item : MonoBehaviour ,I_Interactable{
    [SerializeField] SO_Item so_Item;

    public void Interact(PlayerInteract player) {
        Debug.Log("Adding item : " + so_Item.ItemName);
        player.GetPlayerInventory().AddToInventory(this);
    }

    public string GetName(){
        return so_Item.ItemName;
    }

    public GameObject GetPrefab(){
        return so_Item.ItemPrefab;
    }

    public Sprite GetIcon(){
        return so_Item.ItemIcon;
    }

    public ItemType GetItemType(){
        return so_Item.ItemType;
    }

    public int GetItemMaxInv(){
        return so_Item.InventoryMax;
    }


}
