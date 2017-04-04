using UnityEngine;
using UnityEditor;
using System.Collections;

// Project
// Arch Viz/Project submenu operations
//
public class Project : MonoBehaviour 
{
    [MenuItem("Arch Viz/Project/Create Import Configuration")]
    public static void CreateImportConfig()
    {
        ImportConfiguration asset = ScriptableObject.CreateInstance<ImportConfiguration>();
        asset.Initialize();

        AssetDatabase.CreateAsset(asset, "Assets/Configuration/Import Configuration.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    [MenuItem("Arch Viz/Project/Create Import Sorting Configuration")]
    public static void CreateImportSortingConfig()
    {
        ImportSortingConfigWind window = ScriptableObject.CreateInstance<ImportSortingConfigWind>();
        //window.ShowPopup();
        window.Show();
    }

    [MenuItem("Arch Viz/Project/Create Asset Configuration")]
    public static void CreateAssetConfig()
    {
        AssetConfiguration asset = ScriptableObject.CreateInstance<AssetConfiguration>();
        asset.Initialize();

        AssetDatabase.CreateAsset(asset, "Assets/Configuration/Asset Configuration.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    [MenuItem("Arch Viz/Project/Create Meta-Data Configuration")]
    public static void CreateMetaDataConfig()
    {
        // Test creation of hierarchy

        BIMSubType subType = new BIMSubType();
        subType.Initialize("JL: Sub Type Name");

        BIMType type = new BIMType();
        type.Initialize("JL: Type Name");

        BIMCategory category = new BIMCategory();
        category.Initialize("JL: Category Name");

        BIMData data = ScriptableObject.CreateInstance<BIMData>();
        // data.Initialize("JL: Data Name");

        // Associate objects - just 1:1
        type.SubTypes.Add(subType);

        // type.SubTypes = new BIMSubType[] { subType };
        // category.Types = new BIMType[] { type };
        category.Types.Add(type);
        category.Types2.Add("JL: TYPE 2", type);

        // data.Categories = new BIMCategory[] { category };


        AssetDatabase.CreateAsset(data, "Assets/Configuration/Meta Data Configuration.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        // Selection.activeObject = asset;
    }
}
