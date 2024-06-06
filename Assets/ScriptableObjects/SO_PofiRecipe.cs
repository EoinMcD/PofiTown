using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "ScriptableObjects/PofiRecipe")]

public class SO_PofiRecipe : ScriptableObject {

    [SerializeField] public SO_Item ingrediant1;
    [SerializeField] public SO_Item ingrediant2;
    [SerializeField] public SO_Item ingrediant3;

    [SerializeField] public SO_Item result;


    public SO_Item GetResult() {
        return result;
    }

    public SO_Item GetItem(int i) {
        if (i == 0) return ingrediant1;
        if (i == 1) return ingrediant2;
        if (i == 2) return ingrediant3;

        return null;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SO_PofiRecipe))]
public class PofiRecipeEditor : Editor {


    public override void OnInspectorGUI() {
        serializedObject.Update();

        var pofiRecipe = (SO_PofiRecipe)target;
        //if(pofiRecipe == null) return;

       
        



        GUILayout.Label("RECIPE", new GUIStyle { fontStyle = FontStyle.Bold });
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        Texture texture = null;

        texture = null;
        if (pofiRecipe.ingrediant1 != null) {
            texture = pofiRecipe.ingrediant1.itemIcon.texture;
        }

        GUILayout.Box(texture, GUILayout.Width(100), GUILayout.Height(100));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ingrediant1"), GUIContent.none, true, GUILayout.Width(150));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        texture = null;
        if (pofiRecipe.ingrediant2 != null) {
            texture = pofiRecipe.ingrediant2.itemIcon.texture;
        }

        GUILayout.Box(texture, GUILayout.Width(100), GUILayout.Height(100));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ingrediant2"), GUIContent.none, true, GUILayout.Width(150));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        texture = null;
        if (pofiRecipe.ingrediant3 != null) {
            texture = pofiRecipe.ingrediant3.itemIcon.texture;
        }

        GUILayout.Box(texture, GUILayout.Width(100), GUILayout.Height(100));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ingrediant3"), GUIContent.none, true, GUILayout.Width(150));
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("OUTPUT", new GUIStyle { fontStyle = FontStyle.Bold });
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical();

        if (pofiRecipe.result != null) {
            texture = pofiRecipe.result.itemIcon.texture;
        }

        GUILayout.Box(texture, GUILayout.Width(100), GUILayout.Height(100));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("result"), GUIContent.none, true, GUILayout.Width(150));
        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
