using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public SO_Item so_Item;

    [Header("UI")]
    [SerializeField] Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] public TMP_Text countText;
    [HideInInspector] public int count =1;


    public void InitialiseItem(SO_Item newItem){
        so_Item = newItem;
        image.sprite = newItem.itemIcon;
        RefreshCount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget=false;
        Debug.Log("being drag");
        parentAfterDrag=transform.parent;
        transform.SetParent(transform.root);
        
    }

    public void RefreshCount(){
        
        countText.text = count.ToString();
        bool textActive = count >1;
        countText.gameObject.SetActive(textActive);
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
