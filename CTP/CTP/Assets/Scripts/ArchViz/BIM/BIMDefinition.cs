using UnityEngine;
using System;

// [System.Serializable]	// Ensure property is serializable and will show in the property inspector
public class BIMDefinition : MonoBehaviour
{
    // Correspond to the Revit Category, Family Name, Type
    public string Category;
    public string Type;
    public string SubType; 

    public override string ToString()
    {
        String s = String.Format("Category: {0}\nType: {1}\nSub Type: {2}\nBIM ID: {3}\n",
                                   Category,
                                   Type,
                                   SubType);
        return s;
    }

}
