using NUnit.Framework.Interfaces;
using UnityEditor;
using UnityEngine;
using System.IO;

public class ItemCreatorEditor : EditorWindow {
    private string newItemName = "New Item";
    private SO_Item selectedItemData;
    private GameObject selectedItem;
    private GameObject createdPrefab;

    [MenuItem("Tools/Item Creator")]
    public static void ShowWindow() {
        GetWindow<ItemCreatorEditor>("Item Creator");
    }

    private void OnGUI() {
        GUILayout.Label("Create New Item", EditorStyles.boldLabel);

        newItemName = EditorGUILayout.TextField("Item Name", newItemName);

        if (GUILayout.Button("Create New Item")) {
            CreateNewItem();
        }

        GUILayout.Space(20);
        GUILayout.Label("Edit Selected Item", EditorStyles.boldLabel);

        selectedItem = (GameObject)EditorGUILayout.ObjectField("Item GameObject", selectedItem, typeof(GameObject), true);

        if (selectedItem != null) {
            Item itemComponent = selectedItem.GetComponent<Item>();
            if (itemComponent != null) {
                selectedItemData = itemComponent.GetSO();
                if (selectedItemData != null) {
                    DrawItemDataEditor(selectedItemData);
                }
                else {
                    if (GUILayout.Button("Create New Item Data")) {
                        CreateNewItemData(itemComponent);
                    }
                }
            }
            else {
                GUILayout.Label("Selected GameObject does not have an Item component.");
            }
        }

        GUILayout.Space(20);
        GUILayout.Label("Edit Created Prefab", EditorStyles.boldLabel);

        if (selectedItem != null) {
            Item prefabItemComponent = selectedItem.GetComponent<Item>();
            if (prefabItemComponent != null) {
                    DrawItemDataEditor(prefabItemComponent);
            }
            else {
                GUILayout.Label("Selected prefab does not have an Item component.");
            }
        }
    
    }

    private void CreateNewItem() {
        GameObject newItem = new GameObject(newItemName);
        Item itemComponent = newItem.AddComponent<Item>();

        SO_Item newItemData = CreateInstance<SO_Item>();
        Directory.CreateDirectory($"Assets/Prefabs/Items/{newItemName}");
        string path = $"Assets/Prefabs/Items/{newItemName}/{newItemName}.asset";
        AssetDatabase.CreateAsset(newItemData, path);
        AssetDatabase.SaveAssets();

        itemComponent.SetSO(newItemData);
        EditorUtility.SetDirty(newItem);
        
        string prefabPath = $"Assets/Prefabs/Items/{newItemName}/{newItemName}.prefab";
        selectedItem = PrefabUtility.SaveAsPrefabAsset(newItem, prefabPath);
        DestroyImmediate(newItem);
    }

    private void CreateNewItemData(Item itemComponent) {
            SO_Item newItemData = CreateInstance<SO_Item>();
            string path = $"Assets/Prefabs/Items/{itemComponent.gameObject.name}/{itemComponent.gameObject.name}_Data.asset";
            AssetDatabase.CreateAsset(newItemData, path);
            AssetDatabase.SaveAssets();

            itemComponent.SetSO(newItemData);
            EditorUtility.SetDirty(itemComponent.gameObject);


        
    }

    private void DrawItemDataEditor(SO_Item itemData) {
        SerializedObject serializedItemData = new SerializedObject(itemData);
        SerializedProperty property = serializedItemData.GetIterator();
        property.NextVisible(true);

        while (property.NextVisible(false)) {
            EditorGUILayout.PropertyField(property, true);
        }

        serializedItemData.ApplyModifiedProperties();
    }

    private void DrawItemDataEditor(Item itemData) {
        SerializedObject serializedItemData = new SerializedObject(itemData);
        SerializedProperty property = serializedItemData.GetIterator();
        property.NextVisible(true);

        while (property.NextVisible(false)) {
            EditorGUILayout.PropertyField(property, true);
        }

        serializedItemData.ApplyModifiedProperties();
    }

}
