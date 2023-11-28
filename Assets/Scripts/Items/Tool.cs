using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType{
    Wood,
    Stone,
    Gem
}

public class Tool : ItemAbility
{
    [SerializeField] ResourceType canHarvestResource;
    
    public override void UseItem(){
        Debug.Log("CAN HARVEST: " + canHarvestResource);
    }

    public override void HI(){
        Debug.Log("tool");
    }

}
