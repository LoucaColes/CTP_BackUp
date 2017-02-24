using UnityEngine;
using System;

/// AssetConfiguration
/// Configuration of asset folder and file locations.
/// Persisted as an asset and can be changed within editor and runtime.
/// 
[System.Serializable]
public class AssetConfiguration : ScriptableObject
{
    // CONSTANTS
    const string AssetPrefabPath = "/Assets/Prefabs";
    const string AssetImportPath = "/Assets/Import";
    const string AssetImportFileName = "ArchViz_R2WithTerrain";

    public string prefabPath;
    public string importPath;
    public string importFilename;


    public void Initialize()
    {
        PrefabPath = AssetPrefabPath;
        ImportPath = AssetImportPath;
        ImportFilename = AssetImportFileName;
    }
  
    public string PrefabPath
    {
        get { return prefabPath; }
        set { prefabPath = value; }
    }

    public string ImportPath
    {
        get { return importPath; }
        set { importPath = value; }
    }


    public string ImportFilename
    {
        get { return importFilename; }
        set { importFilename = value; }
    }

    public override string ToString()
    {
        String s = String.Format("Prefab Path: {0}\nImport Path: {1}\nImport Filename: {2}\n",
                                   PrefabPath,
                                   ImportPath,
                                   ImportFilename);

        return s;
    }
}
