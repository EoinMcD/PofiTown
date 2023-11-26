using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] KeyCode interactKey = KeyCode.E;
    [SerializeField] Transform castPoint;

    [SerializeField] GameObject mainInvTemp;

    PlayerCam playerCam;

    bool canInteract=false;
    I_Interactable interactable;

    private void Start() {
        playerCam=GetComponent<PlayerCam>();
        mainInvTemp.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(interactKey)) {
            if(canInteract) {
                interactable.Interact(this);
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab)){
            mainInvTemp.SetActive(!mainInvTemp.activeInHierarchy);
        }
        
    } 

    private void OnTriggerEnter(Collider other) {
        other.gameObject.TryGetComponent<I_Interactable>(out I_Interactable interactable);
        if(interactable!=null) {
            canInteract=true;
            interactable.PreInteract();
            this.interactable = interactable;
            
            
        }
    }

    private void OnTriggerExit(Collider other) {
        other.gameObject.TryGetComponent<I_Interactable>(out I_Interactable interactable);
        if(interactable!=null) {
            canInteract=false;
            interactable.UnInteract();
            this.interactable=null;
            
        }
    }
    
    
}
