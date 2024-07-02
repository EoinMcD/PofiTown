using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Item Database")]
public class ItemDatabase : ScriptableObject {
    public List<Item> items;
}
