using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] Transform itemSpawnPoint;

    Item currentItem;


    public void SpawnItem(SO_Item item) {
        if(currentItem!=null && currentItem !=item.itemPrefab){
            Destroy(itemSpawnPoint.GetChild(0).gameObject);
        }
        currentItem=item.GetItem();
        Instantiate(currentItem, itemSpawnPoint.position,itemSpawnPoint.rotation,itemSpawnPoint);
    }

    public void UseItem(){
        currentItem.UseItem();
    }

    
}
