using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Utilities;

public class DSSingleChoiceNode : DS_Node {


    public override void Initialize(DSGraphView dsGraphview, Vector2 position) {
        base.Initialize(dsGraphview, position);
        DialogueType = Enumerations.DSDialogueType.SingleChoice;

        Choices.Add("Next Dialogue");

    }



    public override void Draw() {
        base.Draw();

        foreach(string choice in Choices) {
            Port choicePort = this.CreatePort(choice);

            choicePort.portName = choice;

            outputContainer.Add(choicePort);

            RefreshExpandedState();

        }

    }




    
}
