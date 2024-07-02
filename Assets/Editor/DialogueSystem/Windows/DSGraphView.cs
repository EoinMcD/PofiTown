using Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class DSGraphView : GraphView{

    DSEditorWindow editorWindow;

    DSSearchWindow searchWindow;

    private SerializableDictionary<string, DSNodeErrorData> ungroupedNodes;

    public DSGraphView(DSEditorWindow dsEditorWindow) {
        this.editorWindow = dsEditorWindow;
        ungroupedNodes = new SerializableDictionary<string, DSNodeErrorData>();


        AddGridBackground();
        AddSearchWindow();
        AddStyles();
        AddManipulators();
        OnElementsDeleted();
    }

    

    #region Override Methods
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
        List<Port> compatablePorts = new List<Port>();

        ports.ForEach(port => {
            if (startPort == port) {
                return;
            }
            if (startPort.node == port.node) {
                return;
            }
            if (startPort.direction == port.direction) {
                return;
            }
            compatablePorts.Add(port);
        });

        return compatablePorts;
    }

    #endregion


    #region Manipluators
    private void AddManipulators() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);


        this.AddManipulator(CreateNodeContextualMenu("Add Node (Single)",DSDialogueType.SingleChoice));
        this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple)", DSDialogueType.MultipleChoice));

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(CreateGroupContextualMenu());
        
    }

    private IManipulator CreateGroupContextualMenu() {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Dialogue Group", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );

        return contextualMenuManipulator;
    }

   

    private IManipulator CreateNodeContextualMenu(string actionTitle, DSDialogueType dialogueType) {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );

        return contextualMenuManipulator;
    }

    #endregion


    #region Elements Creation
    public DS_Node CreateNode(DSDialogueType dialogueType, Vector2 position) {
        Type nodeType = Type.GetType($"DS{dialogueType}Node");

        DS_Node node = (DS_Node)Activator.CreateInstance(nodeType);
        node.Initialize(this,position);
        node.Draw();

        AddUngroupedNode(node);

        return node;
    }

    public Group CreateGroup(string title, Vector2 localMousePosition) {
        Group group = new Group() {
            title = title
        };
        group.SetPosition(new Rect(localMousePosition, Vector2.zero));

        return group;
    }

    #endregion

    #region Repeated Elements
    public void AddUngroupedNode(DS_Node node) {
        string nodeName = node.DialogueName;

        if(!ungroupedNodes.ContainsKey(nodeName)) {
            DSNodeErrorData nodeErrorData = new DSNodeErrorData();

            nodeErrorData.nodes.Add(node);  

            ungroupedNodes.Add(nodeName, nodeErrorData);

            return;
        }

        ungroupedNodes[nodeName].nodes.Add(node);

        Color errorColor = ungroupedNodes[nodeName].errorData.color;

        node.SetErrorStyle(errorColor);

        if (ungroupedNodes[nodeName].nodes.Count ==2) {
            ungroupedNodes[nodeName].nodes[0].SetErrorStyle(errorColor);
        }
    }

    public void RemoveUngroupedNode(DS_Node node) {
        string nodeName = node.DialogueName;

        ungroupedNodes[nodeName].nodes.Remove(node);
        node.ResetStyle();

        if (ungroupedNodes[nodeName].nodes.Count == 1) {
            ungroupedNodes[nodeName].nodes[0].ResetStyle();
            return;
        }

        if (ungroupedNodes[nodeName].nodes.Count == 0) {
            ungroupedNodes.Remove(nodeName);
        }
    }

    #endregion

    #region Elements Addition
    private void AddStyles() {
        this.AddStyleSheets(
            "DialogueSystem/DSGraphViewStyles.uss",
            "DialogueSystem/DSNodeStyles.uss");
    }

    private void AddGridBackground() {
        GridBackground gridBackground = new GridBackground();

        gridBackground.StretchToParentSize();

        Insert(0, gridBackground);
    }

    private void AddSearchWindow() {
        if(!searchWindow) {
            searchWindow=ScriptableObject.CreateInstance<DSSearchWindow>();

            searchWindow.Initialize(this);
        }

        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition),searchWindow);
    }
    #endregion

    #region Callbacks
    private void OnElementsDeleted() {
        deleteSelection = (operationName, askUser) => {
            List<DS_Node> nodesToDelete = new List<DS_Node>();
            foreach (GraphElement element in selection) {
                if (element is DS_Node) {
                    nodesToDelete.Add((DS_Node) element);
                    continue;
                }
            }

            foreach(DS_Node node in nodesToDelete) {
                RemoveUngroupedNode(node);


                RemoveElement(node);    
            }
        };
     }


    #endregion

    #region Utilities

    public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false) {
        Vector2 worldMousePosition = mousePosition;
        if(isSearchWindow) {
            worldMousePosition -= editorWindow.position.position;
        }
        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

        return localMousePosition;



    }


    #endregion
}
