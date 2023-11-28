using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : ItemAbility
{
    [SerializeField] float health;
    [SerializeField] float stamina;

    [SerializeField] bool hasExtraAbility;
    [SerializeField] float duration;


    public override void UseItem(){
        Debug.Log("Health: " + health + ". Stamina: " + stamina);
    }



}
