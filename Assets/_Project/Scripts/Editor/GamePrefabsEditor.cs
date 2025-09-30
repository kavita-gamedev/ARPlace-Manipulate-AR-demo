using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(GamePrefabs))]
public class GamePrefabsEditor : Editor
{
    ReorderableList __UtilityList;
    void OnEnable()
    {
        __UtilityList = new ReorderableList(
            serializedObject,
            serializedObject.FindProperty("UtilityDataInfo"),
            true, true, true, true
        );

        // Header
        __UtilityList.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Utility Data Info");
        };

        // Element layout
        __UtilityList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = __UtilityList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            // --- ID ---
            var idRect = new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(idRect, element.FindPropertyRelative("id"), GUIContent.none);

            // --- Name ---
            var nameRect = new Rect(idRect.xMax + 5, rect.y, 150, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(nameRect, element.FindPropertyRelative("name"), GUIContent.none);

            // --- Object ---
            var objRect = new Rect(nameRect.xMax + 5, rect.y, 80, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(objRect, element.FindPropertyRelative("obj"), GUIContent.none);

            // --- Image ---
            var imgRect = new Rect(objRect.xMax + 5, rect.y, 60, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(imgRect, element.FindPropertyRelative("img"), GUIContent.none);

            // --- ScenarioDatas (nested list) ---
            rect.y += EditorGUIUtility.singleLineHeight + 4;
            var scenarioProp = element.FindPropertyRelative("scenarioDatas");
            var scenarioHeight = EditorGUI.GetPropertyHeight(scenarioProp, true);
            var scenarioRect = new Rect(rect.x, rect.y, rect.width, scenarioHeight);
            EditorGUI.PropertyField(scenarioRect, scenarioProp, new GUIContent("Scenarios"), true);
        };

        // Element height including nested lists
        __UtilityList.elementHeightCallback = index =>
        {
            var element = __UtilityList.serializedProperty.GetArrayElementAtIndex(index);
            var scenarioProp = element.FindPropertyRelative("scenarioDatas");
            float scenarioHeight = EditorGUI.GetPropertyHeight(scenarioProp, true);
            return EditorGUIUtility.singleLineHeight + 4 + scenarioHeight + 6; // padding
        };
    
    }

    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
     
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SelectedUtility"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        __UtilityList.DoLayoutList();
        


        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            //Save the changes to a scriptable object
            EditorUtility.SetDirty(target);
        }
    }
}
