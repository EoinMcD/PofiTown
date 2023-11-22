using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCName{
    Harry,
    Sam,
    Grace
}

// HOLD ALL DIALOGUES FOR ALL NPCS
// CHECKS AGAINST ENUM NPCNAME TO GET RIGHT DIALOGUE
// THIS IS CALLED AT THE START OF THE GAME TO POPULATE NPC DIALOGUE
public class DialogueManager : MonoBehaviour
{
    [SerializeField] SO_Dialogue[] harryDialogue;

    [SerializeField] SO_Dialogue[] graceDialogue;




    public SO_Dialogue[] SetDialogue(NPCName npcName) {
        switch(npcName){
            case NPCName.Harry:
                return harryDialogue;
            case NPCName.Grace:
                return graceDialogue;
            default:
                return null;
        }
    }
    
}
