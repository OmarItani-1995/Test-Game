using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_SaveLoadTester : EditorWindow
{
    private SaveLoad_Base saveLoadManager;
    private SaveLoad_Type currentType = SaveLoad_Type.File;
    
    private string testStringKey;
    private string testStringValue;
    private int testIntKey;
    private int testIntValue;
    private int testFloatKey;
    private float testFloatValue;
    private string testBoolKey;
    private bool testBoolValue;

    private string loadStringKey;
    private string loadedStringValue;
    private string loadIntKey;
    private string loadedIntValue;
    private string loadFloatKey;
    private string loadedFloatValue;
    private string loadBoolKey;
    private string loadedBoolValue;
    
    
    [MenuItem("Tools/SaveLoad Tester")]
    public static void ShowWindow()
    {
        GetWindow<Editor_SaveLoadTester>("SaveLoad Tester");
    }

    private void OnEnable()
    {
        OnCurrentTypeChanged();
    }

    private void OnCurrentTypeChanged()
    {
        switch (currentType)
        {
            case SaveLoad_Type.File:
                    saveLoadManager = new SaveLoadManager_File();
                break;
            case SaveLoad_Type.PlayerPrefs:
                    saveLoadManager = new SaveLoadManager_PlayerPrefs();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Save/Load Tester", EditorStyles.boldLabel);
        
        EditorGUI.BeginChangeCheck();
        currentType = (SaveLoad_Type)EditorGUILayout.EnumPopup("SaveLoad Type", currentType);
        if (EditorGUI.EndChangeCheck())
        {
            OnCurrentTypeChanged();
        }

        DrawSaveSection();
        EditorGUILayout.Space(10);
        
        DrawLoadSection();
    }

    private void DrawLoadSection()
    {
        EditorGUILayout.LabelField("Load");
        EditorGUILayout.BeginHorizontal();
        loadStringKey = EditorGUILayout.TextField("String Key", loadStringKey);
        if (GUILayout.Button("Load String"))
        {
            loadedStringValue = saveLoadManager.LoadString(loadStringKey, "");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Loaded String Value: " + loadedStringValue);

        EditorGUILayout.BeginHorizontal();
        loadIntKey = EditorGUILayout.TextField("Int Key", loadIntKey);
        if (GUILayout.Button("Load Int"))
        {
            loadedIntValue = saveLoadManager.LoadInt(loadIntKey, 0).ToString();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Loaded Int Value: " + loadedIntValue);

        EditorGUILayout.BeginHorizontal();
        loadFloatKey = EditorGUILayout.TextField("Float Key", loadFloatKey);
        if (GUILayout.Button("Load Float"))
        {
            loadedFloatValue = saveLoadManager.LoadFloat(loadFloatKey, 0f).ToString();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Loaded Float Value: " + loadedFloatValue);

        EditorGUILayout.BeginHorizontal();
        loadBoolKey = EditorGUILayout.TextField("Bool Key", loadBoolKey);
        if (GUILayout.Button("Load Bool"))
        {
            loadedBoolValue = saveLoadManager.LoadBool(loadBoolKey, false).ToString();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Loaded Bool Value: " + loadedBoolValue);
    }

    private void DrawSaveSection()
    {
        EditorGUILayout.LabelField("Save");
        EditorGUILayout.BeginHorizontal();
        testStringKey = EditorGUILayout.TextField("String Key", testStringKey);
        testStringValue = EditorGUILayout.TextField("String Value", testStringValue);
        if (GUILayout.Button("Save String"))
        {
            saveLoadManager.SaveString(testStringKey, testStringValue);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        testIntKey = EditorGUILayout.IntField("Int Key", testIntKey);
        testIntValue = EditorGUILayout.IntField("Int Value", testIntValue);
        if (GUILayout.Button("Save Int"))
        {
            saveLoadManager.SaveInt(testIntKey.ToString(), testIntValue);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        testFloatKey = EditorGUILayout.IntField("Float Key", testFloatKey);
        testFloatValue = EditorGUILayout.FloatField("Float Value", testFloatValue);
        if (GUILayout.Button("Save Float"))
        {
            saveLoadManager.SaveFloat(testFloatKey.ToString(), testFloatValue);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        testBoolKey = EditorGUILayout.TextField("Bool Key", testBoolKey);
        testBoolValue = EditorGUILayout.Toggle("Bool Value", testBoolValue);
        if (GUILayout.Button("Save Bool"))
        {
            saveLoadManager.SaveBool(testBoolKey.ToString(), testBoolValue);
        }

        EditorGUILayout.EndHorizontal();
    }
}
