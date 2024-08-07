using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Enumerations;
using Utilities;
using UnityEngine.UIElements;


public class DS_Node : Node
{
    public string DialogueName { get; set; }
    public List<string> Choices { get; set; }

    public string Text {  get; set; }
    public DSDialogueType DialogueType { get; set; }

    private DSGraphView graphview;

    Color defaultBackgroundColor;

    public virtual void Initialize(DSGraphView dsGraphview,Vector2 position) {
        DialogueName = "DialogeName";
        Choices = new List<string>();
        Text = "Dialogue Text";
        graphview=dsGraphview;
        defaultBackgroundColor = new Color(29f/255f, 29f / 255f, 30f / 255f);
        SetPosition(new Rect(position,Vector2.zero));


        mainContainer.AddToClassList("ds-node__main-container");
        extensionContainer.AddToClassList("ds-node__extension-container");
    }

    public virtual void Draw() {
        // TITLE
        TextField dialogueNameTextField = DSElementUtility.CreateTextField(DialogueName, callback => {
            graphview.RemoveUngroupedNode(this);

            DialogueName = callback.newValue;

            graphview.AddUngroupedNode(this);
        });
        dialogueNameTextField.AddClasses("ds-node__textfield", 
            "ds-node__filename-textfield", 
            "ds-node__textfield__hidden");


        titleContainer.Insert(0, dialogueNameTextField);

        // INPUT CONTAINER 
        Port inputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);

        inputPort.portName = "Dialogue Connection";

        inputContainer.Add(inputPort);

        //  EXTENSTIONS CONTAINER 

        VisualElement customDataContainer = new VisualElement();

        customDataContainer.AddToClassList("ds-node__custom-data-container");
        Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text");

        TextField textTextField = DSElementUtility.CreateTextField(Text);
        textTextField.AddClasses("ds-node__textfield", 
            "ds-node__quote-textfield");

        textFoldout.Add(textTextField);
        customDataContainer.Add(textFoldout);
        extensionContainer.Add(customDataContainer);


        
    }


    public void SetErrorStyle(Color color) {
        mainContainer.style.backgroundColor = color;
    }

    public void ResetStyle() {
        mainContainer.style.backgroundColor = defaultBackgroundColor;
    }

}
