using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Item")]



public class SO_Item : ScriptableObject {
    
    [Header("Gameplay")]
    [SerializeField] public ItemType itemType;
    [SerializeField] public float range;
    [SerializeField] public bool useItem;

    [SerializeField] public GameObject itemPrefab;


    [Header("UI")]
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public bool stackable = true;
    [SerializeField] public int stackSize;




}



public enum ItemType {
    Weapon,
    Clothing,
    Tool,
    Edible,
    Ingrediant,
    Other
}
