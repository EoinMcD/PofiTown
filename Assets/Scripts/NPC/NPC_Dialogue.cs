using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    [SerializeField] NPCName npcName;
    DialogueManager dialogueManager;
    UI_Dialogue ui_Dialogue;
    SO_Dialogue[] dialogue;


    SO_Dialogue currentDialogue;
    int currentDialogueNum=0;
    [NonSerialized]
    public bool isTalking = false;

    private void Start() {
        dialogueManager=FindObjectOfType<DialogueManager>();
        ui_Dialogue=FindObjectOfType<UI_Dialogue>();
        dialogue = dialogueManager.SetDialogue(npcName);
    }

    int GetRandomDialogue(){
        return UnityEngine.Random.Range(0,dialogue.Length);
    }

    public void StartTalk(){
        if(isTalking) {return;}

        isTalking=true;
        currentDialogue=dialogue[GetRandomDialogue()];
        ui_Dialogue.SetText(currentDialogue.messages[0]);
        currentDialogueNum++;
    }

    public void ContinueTalk(){
        Debug.Log(currentDialogueNum + "  asdfhsdfs  " + currentDialogue.messages.Length);
        if(currentDialogueNum>=currentDialogue.messages.Length)
        {
            ResetDialogue();
        }
        else{
            ui_Dialogue.SetText(currentDialogue.messages[currentDialogueNum]);
            currentDialogueNum++;
        }
    }

    public void ResetDialogue()
    {
        currentDialogue = null;
        currentDialogueNum = 0;
        isTalking = false;
        ui_Dialogue.ShowUI(false);
    }





}
