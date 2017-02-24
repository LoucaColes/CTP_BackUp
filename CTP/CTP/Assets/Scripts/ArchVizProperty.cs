using UnityEngine;
//using UnityEditor;
using System.Collections; 
using System;

[System.Serializable]	// Ensure shows in property inspector
public class ArchVizProperty
{
	public string name;
	public string value;
	
	public ArchVizProperty() { }

	public ArchVizProperty(string name, string value)
	{
		this.name = name; 
		this.value = value;
	}
	
	// Properties. 
	public string Name { get; set; }
	public string Value { get; set; }

}


