using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectLinker))]
public class ObjectLinkerEditor : Editor {

    string m_stringToCompare;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        m_stringToCompare = EditorGUILayout.TextField(m_stringToCompare);
        //EditorGUILayout.HelpBox("", MessageType.Info);
        ObjectLinker myScript = (ObjectLinker)target;
        myScript.SetUseCatergory(EditorGUILayout.Toggle("Use Catergory", myScript.GetUseCatergory()));
        if (myScript.GetUseCatergory())
        {
            EditorGUILayout.HelpBox("This will spawn prefabs on any object, in which its catergory contains the string entered", MessageType.Warning);
        }

        myScript.SetUseType(EditorGUILayout.Toggle("Use Type", myScript.GetUseType()));
        if (myScript.GetUseType())
        {
            EditorGUILayout.HelpBox("This will spawn prefabs on any object, in which its type contains the string entered", MessageType.Warning);
        }

        myScript.SetUseSubType(EditorGUILayout.Toggle("Use Sub Type", myScript.GetUseSubType()));
        if (myScript.GetUseSubType())
        {
            EditorGUILayout.HelpBox("This will spawn prefabs on any object, in which its sub type contains the string entered", MessageType.Warning);
        }
        if (GUILayout.Button("Initialise"))
        {
            myScript.Init();
        }
        if (GUILayout.Button("Link Object"))
        {
            myScript.SetStringToCompare(m_stringToCompare);
            myScript.LinkObject();
            m_stringToCompare = null;
        }
        if (GUILayout.Button("Next Prefab"))
        {
            myScript.NextPrefab();
        }
        if (GUILayout.Button("Save as New Prefab"))
        {
            myScript.SaveAsNewPrefab();
        }

    }

}
