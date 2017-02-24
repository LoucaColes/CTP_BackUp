using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CameraRig))]
public class CameraRigEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //EditorGUILayout.HelpBox("", MessageType.Info);
        CameraRig myScript = (CameraRig)target;
        if (GUILayout.Button("Start Rig"))
        {
            myScript.StartCameraRig();
        }
    }
}
