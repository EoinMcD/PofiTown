using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PofiMachine : MonoBehaviour,I_Interactable
{

    [SerializeField] GameObject pofiUI;
    [SerializeField] InventorySlot[] ingrediantSlots;
    [SerializeField] InventorySlot resultSlot;

    [SerializeField] SO_PofiRecipe[] recipes;

    
    void ToggleUI(bool toggle) {
        pofiUI.SetActive(toggle);
    }

    public void UnInteract() {
        ToggleUI(false);
    }

    public void Interact(PlayerInteract player) {
        ToggleUI(!pofiUI.activeInHierarchy) ;
    }

    SO_Item GetRecipeOutput() {
        foreach (SO_PofiRecipe recipe in recipes) {
            bool recipeWorking = true;
            for(int i=0; i<ingrediantSlots.Length; i++) {
                if(recipe.GetItem(i) != ingrediantSlots[i].GetItemInSlot() && recipeWorking) {
                    recipeWorking = false; 
                    break;
                }
            }
            if(recipeWorking) {
                return recipe.GetResult();
            }
        }
        return null;
    } 

    public void CraftButton() {
        SO_Item itemToCraft = GetRecipeOutput();
        if (itemToCraft) {
            if (resultSlot.GetItemInSlot() == itemToCraft) {
                InventoryItemUI itemInSlot = resultSlot.GetComponentInChildren<InventoryItemUI>();
                itemInSlot.count++;
                itemInSlot.RefreshCount();
            }
            else {
                resultSlot.AddItem(itemToCraft);
            }


            
        }
        
    }
}
