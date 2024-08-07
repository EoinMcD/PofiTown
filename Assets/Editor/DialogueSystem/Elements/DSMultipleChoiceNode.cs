using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class DSMultipleChoiceNode : DS_Node {




    public override void Initialize(DSGraphView dsGraphview, Vector2 position) {
        base.Initialize(dsGraphview,position);

        DialogueType = Enumerations.DSDialogueType.MultipleChoice;

        Choices.Add("New Choice");
    }




    public override void Draw() {
        base.Draw();

        Button addChoiceButton = DSElementUtility.CreateButton("Add Choice", () => {
            Port choicePort = CreateChoicePort("New Choice");
            Choices.Add("New Choice");

            outputContainer.Add(choicePort);
        });
        addChoiceButton.AddToClassList("ds-node__button");
        mainContainer.Insert(1,addChoiceButton);

        foreach (string choice in Choices) {
            Port choicePort = CreateChoicePort("Choice");

            outputContainer.Add(choicePort);

            RefreshExpandedState();

        }

    }
    #region Element Creation

    private Port CreateChoicePort(string choice) {
        Port choicePort = this.CreatePort();
        InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

        choicePort.portName = "";

        Button deleteChoiceButton = DSElementUtility.CreateButton("X");
        deleteChoiceButton.AddToClassList("ds-node__button");
        TextField choiceTextField = DSElementUtility.CreateTextField(choice);

        choiceTextField.AddClasses("ds-node__textfield",
            "ds-node__choice-textfield",
            "ds-node__textfield__hidden");

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }

    #endregion

}
