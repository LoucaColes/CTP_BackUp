using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MaterialChangerScript))]
public class MaterialChanger : Editor {

    public override void OnInspectorGUI()
    {
        MaterialChangerScript myScript = (MaterialChangerScript)target;
        EditorGUILayout.HelpBox("Choose a system of selecting which objects to change", MessageType.Info);
        EditorGUILayout.HelpBox("Put Editor in Playmode if you don't want changes to be permanant", MessageType.Warning);
        if (myScript.m_matIDs.Length < 1)
        {
            EditorGUILayout.HelpBox("Add more material ids if the objects you want to change has more than one material", MessageType.Warning);
        }
        DrawDefaultInspector();
        
        
        myScript.SetUseTags(EditorGUILayout.Toggle("Use Tags", myScript.GetUseTags()));
        if (myScript.GetUseTags())
        {
            EditorGUILayout.HelpBox("This will change any object, in which its tag is the same as the tag entered", MessageType.Warning);
        }
        myScript.SetUseString(EditorGUILayout.Toggle("Use String", myScript.GetUseString()));
        if (myScript.GetUseString())
        {
            EditorGUILayout.HelpBox("This will change any object, in which its name contains the string entered", MessageType.Warning);
        }
        myScript.SetUseSelection(EditorGUILayout.Toggle("Use Selection", myScript.GetUseSelection()));
        if (myScript.GetUseSelection())
        {
            EditorGUILayout.HelpBox("This will change any object placed within the array", MessageType.Warning);
        }
        myScript.SetUseCatergory(EditorGUILayout.Toggle("Use Catergory", myScript.GetUseCatergory()));
        if (myScript.GetUseCatergory())
        {
            EditorGUILayout.HelpBox("This will change any object, in which its catergory contains the string entered", MessageType.Warning);
        }

        myScript.SetUseType(EditorGUILayout.Toggle("Use Type", myScript.GetUseType()));
        if (myScript.GetUseType())
        {
            EditorGUILayout.HelpBox("This will change any object, in which its type contains the string entered", MessageType.Warning);
        }

        myScript.SetUseSubType(EditorGUILayout.Toggle("Use Sub Type", myScript.GetUseSubType()));
        if (myScript.GetUseSubType())
        {
            EditorGUILayout.HelpBox("This will change any object, in which its sub type contains the string entered", MessageType.Warning);
        }
        
        if (GUILayout.Button("Get Objects To Change"))
        {
            myScript.GetObjects();
        }
        if (GUILayout.Button("Clear Objects List"))
        {
            myScript.ClearList();
        }
        if (GUILayout.Button("Change Materials"))
        {
            myScript.ChangeMaterials();
        }
        if (GUILayout.Button("Save as New Prefab"))
        {
            myScript.SaveAsNewPrefab();
        }
    }
}
