using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(AppDataPrefab))]
public class AppDataPrefabEditor : Editor
{

    ReorderableList __LanguageList;
    ReorderableList __LevelList;
    ReorderableList __TutorialList;
    

    void OnEnable()
    {
        __LanguageList = new ReorderableList(serializedObject, serializedObject.FindProperty("LanguageDataInfo"), true, true, true, true);
        
        __LanguageList.drawHeaderCallback = (delegate (Rect rect)
        {
            EditorGUI.LabelField(rect, "Language Data Info");

        });

        __LanguageList.drawElementCallback = delegate (Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = __LanguageList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            string[] properties = { "id", "language", "code" };

            float rectwidth = rect.width;
            float mFactor = 1f / (float)properties.Length;
            float finalWidth = rect.width * mFactor;

            for (int i = 0; i < properties.Length; i++)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x + rectwidth * (float)i * mFactor, rect.y, finalWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative(properties[i]), GUIContent.none);
            }
        };

        __LevelList = new ReorderableList(serializedObject,serializedObject.FindProperty("LevelDataInfo"),true, true, true, true);


        __LevelList.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Level Data Info");
        };

        // Draw elements
        __LevelList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = __LevelList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            // --- ID ---
            var idRect = new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(idRect, element.FindPropertyRelative("id"), GUIContent.none);

            // --- Title ---
            var titleRect = new Rect(idRect.xMax + 5, rect.y, 150, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(titleRect, element.FindPropertyRelative("title"), GUIContent.none);

            // --- Body ---
            rect.y += EditorGUIUtility.singleLineHeight + 4;
            var bodyProp = element.FindPropertyRelative("Body");
            var bodyHeight = EditorGUI.GetPropertyHeight(bodyProp, true);
            var bodyRect = new Rect(rect.x, rect.y, rect.width, bodyHeight);
            EditorGUI.PropertyField(bodyRect, bodyProp, new GUIContent("Body"));

            // --- CompanionDataInfo ---
            rect.y += bodyHeight + 4;
            var compProp = element.FindPropertyRelative("CompanionDataInfo");
            var compHeight = EditorGUI.GetPropertyHeight(compProp, true);
            var compRect = new Rect(rect.x, rect.y, rect.width, compHeight);
            EditorGUI.PropertyField(compRect, compProp, new GUIContent("Companions"), true);

             rect.y += compHeight + 4;
            var TutoProp = element.FindPropertyRelative("GameTutorialDataInfo");
            var TutoHeight = EditorGUI.GetPropertyHeight(TutoProp, true);
            var TutoRect = new Rect(rect.x, rect.y, rect.width, TutoHeight);
            EditorGUI.PropertyField(TutoRect, TutoProp, new GUIContent("Tutorial"), true);
        };

        // Dynamic element height
        __LevelList.elementHeightCallback = index =>
        {
            var element = __LevelList.serializedProperty.GetArrayElementAtIndex(index);
            float height = EditorGUIUtility.singleLineHeight + 6; // base for ID + title

            height += EditorGUI.GetPropertyHeight(element.FindPropertyRelative("Body"), true) + 4;
            height += EditorGUI.GetPropertyHeight(element.FindPropertyRelative("CompanionDataInfo"), true) + 4;
            height += EditorGUI.GetPropertyHeight(element.FindPropertyRelative("GameTutorialDataInfo"), true) + 4;

            return height;
        };
    

        __TutorialList = new ReorderableList(serializedObject, serializedObject.FindProperty("TutorialDataInfo"), true, true, true, true);

        __TutorialList.drawHeaderCallback = (delegate (Rect rect)
        {
            EditorGUI.LabelField(rect, "Tutorial Data Info");

        });

        __TutorialList.drawElementCallback = delegate (Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = __TutorialList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            string[] properties = { "id", "Title", "Body" };

            float rectwidth = rect.width;
            float mFactor = 1f / (float)properties.Length;
            float finalWidth = rect.width * mFactor;

            for (int i = 0; i < properties.Length; i++)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x + rectwidth * (float)i * mFactor, rect.y, finalWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative(properties[i]), GUIContent.none);
            }
        };
        
        
    }

    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedCompanion"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        __LanguageList.DoLayoutList();
        

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        __LevelList.DoLayoutList();

         EditorGUILayout.Space();
        EditorGUILayout.Space();
        __TutorialList.DoLayoutList();

    

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            //Save the changes to a scriptable object
            EditorUtility.SetDirty(target);
        }
    }
   
}
