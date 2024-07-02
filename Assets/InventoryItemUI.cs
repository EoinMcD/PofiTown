using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [HideInInspector] public SO_Item so_Item;

    [Header("UI")]
    [SerializeField] Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] public TMP_Text countText;
    [HideInInspector] public int count =1;
    [HideInInspector] public bool empty = true;
     public CurrentlyIn currentlyIn;


    public void InitialiseItem(SO_Item newItem){
        so_Item = newItem;
        image.sprite = newItem.itemIcon;
        RefreshCount();
        GetInventoryType();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget=false;
        countText.raycastTarget = false;
        Debug.Log("being drag");
        parentAfterDrag=transform.parent;
        Debug.Log(transform.root.gameObject.name);
        transform.SetParent(transform.root);
        
    }

    public void RefreshCount(){
        
        countText.text = count.ToString();
        bool textActive = count >1;
        countText.gameObject.SetActive(textActive);
        if(count <= 0) {
            Destroy(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget=true;
        countText.raycastTarget = true;
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        GetInventoryType();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == 0 && Input.GetKey(KeyCode.LeftShift)) {
            Debug.Log("CLICK " + currentlyIn.ToString());
            FindTopSpot().ShiftClick(currentlyIn, this);
        }

        if (eventData.button == 0 && Input.GetKey(KeyCode.LeftControl)) {
            Debug.Log("REMOVE " + currentlyIn.ToString());
            VirtualInventory inv = transform.root.gameObject.GetComponentInChildren<VirtualInventory>();
            inv.RemoveItem(this);
        }

        //if (eventdata.button == pointereventdata.inputbutton.right) {
        //    int tempcount;
        //    if (count % 2 == 0) {
        //        count /= 2;
        //    }
        //    else {
        //        tempcount = count / 2;
        //        count = count / 2 + count % 2;
        //        debug.log("das " + tempcount + "    -   " + count);
        //    }

        //    inventoryitemui newitem = instantiate(gameobject).getcomponent<inventoryitemui>();
        //    findtopspot().addtoinventory(newitem.so_item, )



        //}
    }


    public VirtualInventory FindTopSpot() {
        return transform.root.gameObject.GetComponentInChildren<VirtualInventory>();
        
    }

    private void GetInventoryType() {
        if (transform.root.gameObject.GetComponentInChildren<Chest>()) {
            currentlyIn = CurrentlyIn.Chest;
        }
        else if (transform.root.gameObject.GetComponentInChildren<PlayerInventory>()) {
            if (transform.parent.gameObject.tag == "InvMain") {
                Debug.Log("main");
                currentlyIn = CurrentlyIn.PlayerMain;
            }
            else if (transform.parent.gameObject.tag == "InvHotbar") {
                Debug.Log("HOTBAR");
                currentlyIn = CurrentlyIn.PlayerHotbar;
            }
            else {
                Debug.Log("other!!");
            }

        }
        
    }

}


public enum CurrentlyIn {
    PlayerMain,
    PlayerHotbar,
    Chest
}
