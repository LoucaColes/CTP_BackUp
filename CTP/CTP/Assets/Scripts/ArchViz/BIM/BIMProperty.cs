using UnityEngine;
//using UnityEditor;
using System.Collections;
using System;

[System.Serializable]	// Ensure property is serializable and will show in the property inspector
public class BIMProperty 
{
    public string name;
    public string value;

    public BIMProperty() { } 

    public BIMProperty(string name, string value)
    {
        this.name = name;
        this.value = value;
    }

    // Properties
    public string Name { get; set; }
    public string Value { get; set; }

}