using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interactable 
{
    void Interact(){
        Debug.Log("Not implemented");
    }

    void Interact(PlayerInteract player) {
        Debug.Log("Not implemented");
    }

    void PreInteract(){
        Debug.Log("Not implemented");
    }

    void UnInteract() {
        Debug.Log("Not implemented");
    }

    GameObject GetInteractableObject(){
        Debug.Log("Not implemented");
        return null;
    }
}

