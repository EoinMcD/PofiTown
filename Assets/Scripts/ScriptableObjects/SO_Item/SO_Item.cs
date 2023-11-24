using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
        Weapon,
        Clothes,
        Ingrediant,
        Other
}

[CreateAssetMenu(fileName = "SO_Item", menuName = "PofiPofiPofi/SO_Item", order = 0)]
public class SO_Item : ScriptableObject{
    [SerializeField] string id;
    [SerializeField] string itemName;
    [SerializeField] Sprite itemIcon;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] ItemType itemType;
    [SerializeField] int inventoryMax;

    public GameObject ItemPrefab { get => itemPrefab; }
    public string ItemName { get => itemName;}
    public Sprite ItemIcon { get => itemIcon; }
    public ItemType ItemType { get => itemType; }
    public int InventoryMax { get => inventoryMax; }


    
}
