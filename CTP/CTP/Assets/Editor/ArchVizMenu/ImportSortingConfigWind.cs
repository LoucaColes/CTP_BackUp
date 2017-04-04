using UnityEngine;
using UnityEditor;
using System.Collections;

public class ImportSortingConfigWind : EditorWindow {

    private bool m_useCat, m_useType, m_useSubType, m_closeWindow;
    static ImportSortingConfigWind window;

    public static void Init()
    {
        window = (ImportSortingConfigWind)EditorWindow.GetWindow(typeof(ImportSortingConfigWind));
        window.Show();
        
    }

    private void OnGUI()
    {
        

        GUILayout.Label("Choose how to sort the objects when imported");
        //m_useCat = GUILayout.Toggle(m_useCat, "Use Catorgory");
        //m_useType = GUILayout.Toggle(m_useType, "Use Type");
        //m_useSubType = GUILayout.Toggle(m_useSubType, "Use Sub-Type");
        if (GUILayout.Toggle(m_useCat, "Use Catorgory"))
        {
            m_useCat = true;
            m_useType = false;
            m_useSubType = false;
        }
        if (GUILayout.Toggle(m_useType, "Use Type"))
        {
            m_useCat = false;
            m_useType = true;
            m_useSubType = false;
        }
        if (GUILayout.Toggle(m_useSubType, "Use Sub-Type"))
        {
            m_useCat = false;
            m_useType = false;
            m_useSubType = true;
        }
        if (GUI.Button(new Rect(10, 100, 50, 50), "Close"))
        {
            m_closeWindow = true;
            if (GetUseCat())
            {
                ImportSortingConfig asset = ScriptableObject.CreateInstance<ImportSortingConfig>();
                asset.SetUseCat(GetUseCat());

                AssetDatabase.CreateAsset(asset, "Assets/Configuration/Import Sorting Configuration.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;

                Close();
            }
            if (GetUseType())
            {
                ImportSortingConfig asset = ScriptableObject.CreateInstance<ImportSortingConfig>();
                asset.SetUseType(GetUseType());

                AssetDatabase.CreateAsset(asset, "Assets/Configuration/Import Sorting Configuration.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;

                Close();
            }
            if (GetUseSubType())
            {
                ImportSortingConfig asset = ScriptableObject.CreateInstance<ImportSortingConfig>();
                asset.SetUseSubType(GetUseSubType());
                
                AssetDatabase.CreateAsset(asset, "Assets/Configuration/Import Sorting Configuration.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;

                Close();
            }
        }
    }

    public bool GetUseCat() { return m_useCat; }

    public bool GetUseType() { return m_useType; }

    public bool GetUseSubType() { return m_useSubType; }

    public bool GetCloseWindow() { return m_closeWindow; }
}
