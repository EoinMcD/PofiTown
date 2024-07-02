using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class DSEditorWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/Dialogue Graph")]
    public static void ShowExample()
    {
        GetWindow<DSEditorWindow>("Dialogue Graph");
    }

    private void CreateGUI() {
        AddGraphView();

        AddStyles();
    }

    
    #region Elements Addition 
    private void AddGraphView() {
        DSGraphView graphView = new DSGraphView(this);

        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void AddStyles() {

        rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");

    }

    #endregion
}
