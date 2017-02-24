using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectSorter))]
public class ObjectSorterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //EditorGUILayout.HelpBox("", MessageType.Info);
        ObjectSorter myScript = (ObjectSorter)target;
        if (GUILayout.Button("Init"))
        {
            myScript.Init();
        }
        if (GUILayout.Button("Clear"))
        {
            myScript.Clear();
        }
        if (GUILayout.Button("Get Child Objects"))
        {
            myScript.GetChildObjects();
        }
        if (GUILayout.Button("Sort By Tag"))
        {
            myScript.SortByTags();
        }
    }
}