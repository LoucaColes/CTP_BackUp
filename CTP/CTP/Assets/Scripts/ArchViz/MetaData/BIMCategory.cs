using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable] 
public class BIMCategory : BIMMetaData
{
    [SerializeField]
    public List<BIMType> types;
    [SerializeField]
    public Dictionary<string, BIMType> types2;

    public BIMCategory() 
    {
        types = new List<BIMType>();
        types2 = new Dictionary<string, BIMType>();
    }

    public List<BIMType> Types
    {
        get
        {
            return types;
        }
        set
        {
            types = value;
        }
    }

    public Dictionary<string, BIMType> Types2
    {
        get
        {
            return types2;
        }
        set
        {
            types2 = value;
        }
    }



    // public BIMType[] types;
        /*
    public BIMType[] Types
    {
        get
        {
            return types;
        }
        set
        {
            types = value;
        }
    }
         * */
}
