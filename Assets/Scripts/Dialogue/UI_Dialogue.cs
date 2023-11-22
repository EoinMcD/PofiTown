using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TMP_Text dialogueText;

    private void Start() {
        ShowUI(false);
    }
    public void SetText(String text){
        if(!dialogueBox.activeInHierarchy){
            ShowUI(true);
        }
        dialogueText.SetText(text);
    }

    public void ShowUI(bool show){
        dialogueBox.SetActive(show);
    }
}
