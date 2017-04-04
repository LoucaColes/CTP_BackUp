using UnityEngine;
using System.Collections;

public class ArchVizMetaInfo : MonoBehaviour {

	// Correspond to the Revit Category, Family Name, Type
	public string category;
	public string type;
	public string subType;
	public ArchVizProperty[] Details;

	 
    public override string ToString() 
    {
        string s = "";

        // Display the category, type and subtype
        s += "<b>Category:</b> " + category + "\n";
        s += "<b>Type:</b> " + type + "\n";
        s += "<b>Subtype:</b> " + subType + "\n\n";

        // Iterate through the properties
        foreach (ArchVizProperty archVizProp in Details)
        {
            s += archVizProp.name + ":  " + archVizProp.value + "\n";
        }

        return s;

    }

}


