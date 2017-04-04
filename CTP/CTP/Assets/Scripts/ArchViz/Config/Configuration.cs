#if UNITY_EDITOR
using UnityEditor;
#endif
/// Configuration
/// Provides singleton access to the Configuration objects.
/// 
#if UNITY_EDITOR
public static class Configuration
{
    private static ImportConfiguration ImportConfigInstance = null;
    private static AssetConfiguration AssetConfigInstance   = null;
    private static ImportSortingConfig ImportSortingConfigInstance = null;

    public static ImportConfiguration GetImportConfig()
    {
        if (System.Object.ReferenceEquals(ImportConfigInstance, null))
        {
            string[] searchInFolders = { "Assets/Configuration" };
            string[] GUIDs = AssetDatabase.FindAssets("Import Configuration", searchInFolders);

            // Retrieve the fully qualified path
            string assetPath = AssetDatabase.GUIDToAssetPath(GUIDs[0]);

            // Retrieve the Prefab type - Prefabs are GameObjects
            ImportConfigInstance = (ImportConfiguration)AssetDatabase.LoadAssetAtPath(assetPath, typeof(ImportConfiguration));
        }

        return ImportConfigInstance;
    }

    public static ImportSortingConfig GetImportSortConfig()
    {
        if (System.Object.ReferenceEquals(ImportSortingConfigInstance, null))
        {
            string[] searchInFolders = { "Assets/Configuration" };
            string[] GUIDs = AssetDatabase.FindAssets("Import Sorting Configuration", searchInFolders);

            // Retrieve the fully qualified path
            string assetPath = AssetDatabase.GUIDToAssetPath(GUIDs[0]);

            // Retrieve the Prefab type - Prefabs are GameObjects
            ImportSortingConfigInstance = (ImportSortingConfig)AssetDatabase.LoadAssetAtPath(assetPath, typeof(ImportSortingConfig));
        }

        return ImportSortingConfigInstance;
    }

    public static AssetConfiguration GetAssetConfig()
    {
        if (System.Object.ReferenceEquals(AssetConfigInstance, null))
        {
            string[] searchInFolders = { "Assets/Configuration" };
            string[] GUIDs = AssetDatabase.FindAssets("Asset Configuration", searchInFolders);

            // Retrieve the fully qualified path
            string assetPath = AssetDatabase.GUIDToAssetPath(GUIDs[0]);

            // Retrieve the Prefab type - Prefabs are GameObjects
            AssetConfigInstance = (AssetConfiguration)AssetDatabase.LoadAssetAtPath(assetPath, typeof(AssetConfiguration));
        }

        return AssetConfigInstance;
    }



}
#endif

