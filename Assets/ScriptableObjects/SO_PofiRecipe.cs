using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PofiRecipe")]

public class SO_PofiRecipe : ScriptableObject {

    [SerializeField] SO_Item ingrediant1;
    [SerializeField] SO_Item ingrediant2;
    [SerializeField] SO_Item ingrediant3;

    [SerializeField] SO_Item result;


    public SO_Item GetResult() {
        return result;
    }

    public SO_Item GetItem(int i) {
        if (i == 0) return ingrediant1;
        if (i == 1) return ingrediant2;
        if (i == 2) return ingrediant3;

        return null;
    }
}
