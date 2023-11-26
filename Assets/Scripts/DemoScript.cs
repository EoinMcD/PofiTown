using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    [SerializeField] InventoryManager invManager;
    
    public SO_Item[] itemsToPickup;


    public void PickUpItem(int id) {
        bool result = invManager.AddItem(itemsToPickup[id]);
        if(result) {
            Debug.Log("Item added");
        }
        else
            {Debug.Log("NO ADD");}
    }

    public void GetSelectedItem() {
        SO_Item receivedItem = invManager.GetSelectedItem();
        if(receivedItem!=null){
            Debug.Log("Received item: " + receivedItem);
        }
        else{
            Debug.Log("No item received");
        }
    }
}
