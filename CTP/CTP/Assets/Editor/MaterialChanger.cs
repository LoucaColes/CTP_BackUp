using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MaterialChangerScript))]
public class MaterialChanger : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("Choose a system of selecting which objects to change", MessageType.Info);
        EditorGUILayout.HelpBox("Put Editor in Playmode if you don't want changes to be permanant", MessageType.Warning);
        MaterialChangerScript myScript = (MaterialChangerScript)target;
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
        
    }
}
