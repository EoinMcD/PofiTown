using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAbility : MonoBehaviour
{
    public virtual void UseItem(){
        
    }
    public virtual void HI(){
        Debug.Log("itemability");
    }
}
