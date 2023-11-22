using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Interact : MonoBehaviour, I_Interactable
{
    [SerializeField] Image image;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineTargetGroup targetGroup;

    NPC_Dialogue npc_Dialogue;
    
    

    private void Start() {
        image.gameObject.SetActive(false);
        npc_Dialogue = GetComponent<NPC_Dialogue>();
    }

    public void Interact(PlayerInteract player)
    {
        if(!npc_Dialogue.isTalking) {
            image.gameObject.SetActive(false);
            AddTarget(player.gameObject);
            cam.Priority=200; 
            npc_Dialogue.StartTalk();
        }
        else{
            npc_Dialogue.ContinueTalk();
        }
    }

    public void UnInteract()
    {
        cam.Priority=2; 
        ResetTarget();
        npc_Dialogue.ResetDialogue();
    }

    public void PreInteract(){
        image.gameObject.SetActive(true);
    }

    public GameObject GetInteractableObject(){
        return this.gameObject;
    }

    public void AddTarget(GameObject other){
        targetGroup.AddMember(other.transform,1,1.5f);
    }

    public void ResetTarget(){
        if(targetGroup.m_Targets.Length>1) {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++) {
                if(i!=0) {
                    targetGroup.RemoveMember(targetGroup.m_Targets[i].target);
                }
            }
        }
    }

}
