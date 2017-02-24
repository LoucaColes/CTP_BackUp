using UnityEngine;
using System;

/// ImportConfiguration
/// Configuration of import settings.
/// Persisted as an asset and can be changed within editor and runtime.
/// 
[System.Serializable]
public class ImportConfiguration : ScriptableObject
{
    // CONSTANTS - SPECIFIC TO A REVIT IMPORT
    const string ImportCategory = "Category Name";
    const string ImportType = "Family Name";
    const string ImportSubType = "Type Name";
    const string ImportBIMIdDelimiters = "[]";
    const string ImportNameComponentStartDelimiter = "[";
    const string ImportNameComponentEndDelimiter = "]";
    const string ImportDefaultTag = "Undefined";

    public enum PrefabFolderStructure
    {
        Category,
        CategoryAndType,
        CategoryAndTypeAndSubType
    }

    public string category;
    public string type;
    public string subType;
    public string bimIdDelimiters;  // Used in RegEx to determine the BIM Id
    public string nameComponentStartDelimiter;
    public string nameComponentEndDelimiter;
    public string defaultTag;

    public PrefabFolderStructure organizePrefabFolderStructureBy;

    public void Initialize()
    {
        // Revit defaults
        Category = ImportCategory;
        Type = ImportType;
        SubType = ImportSubType;
        BIMDelimiterCharacters = ImportBIMIdDelimiters;
        NameComponentStartDelimiter = ImportNameComponentStartDelimiter;
        NameComponentEndDelimiter = ImportNameComponentEndDelimiter;
        DefaultTag = ImportDefaultTag;
    }

    // The imported mesh name will contain the BIM object ID
    // The BIM Delimiter Characters distinguish the Object ID        
    public string Category 
    { 
        get
        {
            return category;
        }
        set
        {
            category = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public string SubType
    {
        get
        {
            return subType;
        }
        set
        {
            subType = value;
        }
    }

    public string BIMDelimiterCharacters
    {
        get
        {
            return bimIdDelimiters;
        }
        set
        {
            bimIdDelimiters = value;
        }
    }

    public string NameComponentStartDelimiter
    {
        get
        {
            return nameComponentStartDelimiter;
        }
        set
        {
            nameComponentStartDelimiter = value;
        }
    }

    public string NameComponentEndDelimiter
    {
        get
        {
            return nameComponentEndDelimiter;
        }
        set
        {
            nameComponentEndDelimiter = value;
        }
    }

    public string DefaultTag
    {
        get
        {
            return defaultTag;
        }
        set
        {
            defaultTag = value;
        }
    }

    public override string ToString()
    {
        String s = String.Format("Category: {0}\nType: {1}\nSub Type: {2}\nBIM ID Delimiters: {3}\n", 
                                    Category,
                                    Type,
                                    SubType,
                                    BIMDelimiterCharacters);

        String s2 = String.Format("Name Component Start Delimiter: {0}\nName Component End Delimiter: {1}\nDefault Tag: {2}\n",
                                    NameComponentStartDelimiter,
                                    NameComponentEndDelimiter,
                                    DefaultTag);

        return s + s2;
    }

    // JL: TO ADD Properties to ignore on import, for example 3DS Max properties
}
