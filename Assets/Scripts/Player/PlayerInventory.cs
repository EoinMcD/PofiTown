using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 2;

    Dictionary<Item, int> itemDict = new Dictionary<Item, int>();
    int dupItems = 0;       //This is for if an item exceeds its inventory max size and takes more than 1 slot.     This is then added to the itemDict count to see if there is an available slot open

    private void Update() {
        if(Input.GetKeyDown(KeyCode.V) && itemDict.Count!=0){
            foreach(KeyValuePair<Item, int> items in itemDict){
                Debug.Log("You have "+ items.Value+ " " + items.Key);
            }
        }
    }

    public void AddToInventory(Item item){
        
        /*
            This checks if there is an item of this type already in the inventory. If there is, it is added to the stack.
            If the stack is full it will create a new stack, unless there are no more stack slots.
            If there is no item already in the inventory, it will take a slot for this and add it, unless there are no slots left.

        */

        if(itemDict.ContainsKey(item)) {

            if(itemDict[item]%item.GetItemMaxInv()==0) {
                if(!(itemDict.Count+dupItems>=inventorySize)) {
                    dupItems++;
                    itemDict[item]++;
                }
                else{
                    Debug.Log("CANNOT ADD");
                }
            }
            else{
                itemDict[item]++;
            }
        }
        else{
            if(!(itemDict.Count+dupItems>=inventorySize)) {
                itemDict.Add(item,1);
            }
            else{
                    Debug.Log("CANNOT ADD");
                }
        }    
    }
}
