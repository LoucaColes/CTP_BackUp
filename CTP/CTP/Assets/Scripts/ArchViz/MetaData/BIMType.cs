using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BIMType : BIMMetaData
{
    [SerializeField]
    // public BIMSubType[] subTypes;
    public List<BIMSubType> subTypes;

    public BIMType()
    {
        subTypes = new List<BIMSubType>();
    }

    public List<BIMSubType> SubTypes
    {
        get
        {
            return subTypes;
        }
        set
        {
            subTypes = value;
        }
    }
 

}



