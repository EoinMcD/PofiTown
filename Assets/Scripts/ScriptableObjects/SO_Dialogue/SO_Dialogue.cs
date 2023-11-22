using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SO_Dialogue", menuName = "PofiPofiPofi/SO_Dialogue", order = 0)]
public class SO_Dialogue : ScriptableObject {
    [SerializeField] NPCName npcName;
    [SerializeField] int id;
    [TextArea(2,5)]
    [SerializeField] public string[] messages; 
}

