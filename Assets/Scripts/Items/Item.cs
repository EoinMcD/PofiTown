using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] SO_Item so_Item;
    [SerializeField] ItemAbility itemAbility; 

    public void UseItem(){
        itemAbility.UseItem();
    }
}
