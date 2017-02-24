using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class BIMData : ScriptableObject  
{
    [SerializeField]
    public List<BIMCategory> categories;

    // BIMCategory[] categories;

    private Dictionary<string, BIMMetaData> cache;


   public BIMData()
    {
        categories = new List<BIMCategory>();
    }


    public List<BIMCategory> Categories
    {
        get
        {
            return categories;
        }
        set
        {
            categories = value;
        }
    }

    private string GetCandidateKeyName(GameObject gameObject)
    {
        string key = "Undefined";

        BIMDefinition bimDefinition = gameObject.GetComponent<BIMDefinition>();

        if (bimDefinition is BIMDefinition)
        {
            string objCategory = bimDefinition.Category;
            string objType = bimDefinition.Type;
            string objSubtype = bimDefinition.SubType;

            // Concatenate to produce the key name
            key = objCategory + "." + objType + "." + objSubtype;
        }

        return key;
    }



    ////////////////////////////////////////////////////////////////////////////////////////
    /// CATEGORY
    public string GetCategoryName(BIMDefinition bimDefinition)
    {
        string objCategory = "Undefined";

        // BIMDefinition bimDefinition = gameObject.GetComponent<BIMDefinition>();

        if (bimDefinition is BIMDefinition)
        {
            objCategory = bimDefinition.Category;
        }

        return objCategory;
    }

    //
    public BIMCategory GetBIMCategory(BIMDefinition bimDefinition)
    {
        string categoryName = GetCategoryName(bimDefinition);

        foreach (BIMCategory bimCategory in categories)
        {
            if (bimCategory.Name.Equals(categoryName))
            {
                return bimCategory;
            }
                
        }
        return null;
    }

    //
    public BIMCategory CreateBIMCategory(BIMDefinition bimDefinition)
    {
        string categoryName = GetCategoryName(bimDefinition);

        BIMCategory bimCategory = new BIMCategory();
        bimCategory.Initialize(categoryName);

        // Add to the array
        categories.Add(bimCategory);

        return bimCategory;
    }

    //
    public BIMCategory GetOrCreateBIMCategory(BIMDefinition bimDefinition)
    {
        BIMCategory bimCategory = GetBIMCategory(bimDefinition);

        if (bimCategory == null)
        {
            bimCategory = CreateBIMCategory(bimDefinition);
        }

        return bimCategory;
    }

    /////////////////////////////////////////////////////////////////////

    public string GetTypeName(BIMDefinition bimDefinition)
    {
        // BIMDefinition bimDefinition = gameObject.GetComponent<BIMDefinition>();

        string typeName = null;

        if (bimDefinition is BIMDefinition)
        {
            typeName = bimDefinition.Type;
        }

        return typeName;
    }

    // 
    public BIMType GetBIMType(BIMDefinition bimDefinition,
                              BIMCategory bimCategory)
    {
        string typeName = GetTypeName(bimDefinition);

        foreach (BIMType bimType in bimCategory.Types)
        {
            if (bimType.Name.Equals(typeName))
            {
                return bimType;
            }
                
        }
        return null;
    }

    public BIMType CreateBIMType(BIMDefinition bimDefinition,
                                 BIMCategory bimCategory)
    {
        string typeName = GetTypeName(bimDefinition);

        BIMType bimType = new BIMType();
        bimType.Initialize(typeName);

        // Add to the array
        bimCategory.Types.Add(bimType);

        return bimType;
    }

    public BIMType GetOrCreateBIMType(BIMDefinition bimDefinition,
                                      BIMCategory bimCategory)
    {
        BIMType bimType = GetBIMType(bimDefinition,
                                     bimCategory);

        if (bimType == null)
        {
            bimType = CreateBIMType(bimDefinition,
                                    bimCategory);
        }

        return bimType;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////


    public string GetSubTypeName(BIMDefinition bimDefinition)
    {
        // BIMDefinition bimDefinition = gameObject.GetComponent<BIMDefinition>();

        string subTypeName = null;

        if (bimDefinition is BIMDefinition)
        {
            subTypeName = bimDefinition.SubType;
        }

        return subTypeName;
    }

    // 
    public BIMSubType GetBIMSubType(BIMDefinition bimDefinition,
                                 BIMType bimType)
    {
        string subTypeName = GetSubTypeName(bimDefinition);

        foreach (BIMSubType bimSubType in bimType.SubTypes)
        {
            if (bimSubType.Name.Equals(subTypeName))
            {
                return bimSubType;
            }
                
        }
        return null;
    }

    public BIMSubType CreateBIMSubType(BIMDefinition bimDefinition,
                                    BIMType bimType)
    {
        string subTypeName = GetSubTypeName(bimDefinition);

        BIMSubType bimSubType = new BIMSubType();
        bimSubType.Initialize(subTypeName);

        // Add to the array
        bimType.SubTypes.Add(bimSubType);

        return bimSubType;
    }

    public BIMSubType GetOrCreateBIMSubType(BIMDefinition bimDefinition,
                                          BIMType bimType)
    {
        BIMSubType bimSubType = GetBIMSubType(bimDefinition,
                                              bimType);

        if (bimSubType == null)
        {
            bimSubType = CreateBIMSubType(bimDefinition,
                                          bimType);
        }

        return bimSubType;
    }




    ///////////////////////////////////////////////////////////////////////////////////////////////


    public void Create(GameObject go)
    {
        // Tree structure
        string objCategory = "Undefined";

        BIMDefinition bimDefinition = go.GetComponent<BIMDefinition>();

        if (bimDefinition is BIMDefinition)
        {
            BIMCategory bimCategory = GetOrCreateBIMCategory(bimDefinition);

            string objType = bimDefinition.Type;
            if (!String.IsNullOrEmpty(objType))
            {
                // Create the associated type
                BIMType bimType = GetOrCreateBIMType(bimDefinition,
                                                     bimCategory);

                string objSubtype = bimDefinition.SubType;

                if (!String.IsNullOrEmpty(objSubtype))
                {
                    // Create the associated type
                    BIMSubType bimSubType = GetOrCreateBIMSubType(bimDefinition,
                                                                  bimType);
                }
            }
        }
        else
        {
            BIMDefinition bimDefUndefined = new BIMDefinition();
            bimDefUndefined.Category = objCategory;

            BIMCategory bimCategory = GetOrCreateBIMCategory(bimDefUndefined);
        }
    }



}
