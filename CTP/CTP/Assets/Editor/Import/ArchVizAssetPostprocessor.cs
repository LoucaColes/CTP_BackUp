using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

public class ArchVizAssetPostprocessor : AssetPostprocessor {

    [SerializeField]
    private List<GameObject> m_childObjects;
    [SerializeField]
    private List<GameObject> m_parentsCreated;
    private GameObject m_mainParent;
    private bool m_sortByCat = false, m_sortByType = false, m_sortBySubType = true;

    private string GetBIMId(GameObject go)
    {
        string goName = go.name;
        string goNameCopy = string.Copy(goName);

        // Regex regex = new Regex("\[\]");
        // string[] lines = Regex.Split(goNameCopy, "\[\]");
        // string words = "This is a list of words, with: a bit of punctuation" +
        //                       "\tand a tab character.";

        string [] split = goNameCopy.Split(new Char [] {'[', ']'});
        /*
        foreach (string s in split) 
        {
            if (s.Trim() != "")
                Console.WriteLine(s);
            }
        */

        // Tokenize based on the import options - delimeter characters
        /*
        string[] lines = Regex.Split(goNameCopy,
                                     Configuration.GetImportConfig().BIMDelimiterCharacters);
        */

        string bimId = null;

        // Return the last token
        //if (split.Length > 0)
        //{
        //    bimId = split[split.Length - 2];
        //    bimId.Trim();
        //}

        return bimId;
    }

    private string BuildComponentName(string component)
    {
        string componentName = "";

        if (!string.IsNullOrEmpty(component))
        {
            string nameComponentStartDelimiter = Configuration.GetImportConfig().NameComponentStartDelimiter;
            string nameComponentEndDelimiter = Configuration.GetImportConfig().NameComponentEndDelimiter;

            componentName = string.Format("{0}{1}{2}", nameComponentStartDelimiter,
                                                       component,
                                                       nameComponentEndDelimiter);
        }

        return componentName;
    }

    private string GetName(GameObject go,
                           BIMDefinition bimDefinition)
    {
        string objectName = "";
        /*
        if (String.IsNullOrEmpty(bimDefinition.Category) &&
            String.IsNullOrEmpty(bimDefinition.Type) &&
            String.IsNullOrEmpty(bimDefinition.SubType))
        {
            objectName = go.name;
        }
        else
        {
         * */
            string category = BuildComponentName(bimDefinition.Category);
            string type = BuildComponentName(bimDefinition.Type);
            string subType = BuildComponentName(bimDefinition.SubType);

            string bimID = BuildComponentName(GetBIMId(go));

            objectName = string.Format("{0}{1}{2}{3}",   category,
                                                         type,
                                                         subType,
                                                         bimID);
        // }

        return objectName;
    }

    void OnPostprocessGameObjectWithUserProperties(GameObject go,
                                                    string[] names,
                                                    object[] values)
    {
        ImportConfiguration importConfig = Configuration.GetImportConfig();
        /*
        if (go.name == "3dSolid.01")
        {
            int i;
            i = 0;
        }
         */

        // BIMDefinition bimDef = new BIMDefinition();
        List<BIMProperty> bimProperties = new List<BIMProperty>();

        string name, value;
        string category = null;
        string type = null;
        string subType = null;

        // Process the user properties
        for (int i = 0; i < names.Length; i++)
        {
            name = names[i];
            value = values[i].ToString();

            // Process the Category, Type and SubType
            if (name.Equals(importConfig.Category))
            {
                // bimDef.Category = value;
                category = value;
                for (int j = 0; j < m_parentsCreated.Count; j++)
                {
                    if (category == m_parentsCreated[j].name)
                    {
                        go.transform.parent = m_parentsCreated[j].transform;
                    }
                }
                continue;
            }
            else if (name.Equals(importConfig.Type))
            {
                // bimDef.Type = value;
                type = value;
                continue;
            }
            else if (name.Equals(importConfig.SubType))
            {
                // bimDef.SubType = value;
                subType = value;
                continue;
            }
            
            BIMProperty bimProperty = new BIMProperty(name, value);
            bimProperties.Add(bimProperty);
        }

        bool bimOriginated = true;

        if (String.IsNullOrEmpty(category) &&
            String.IsNullOrEmpty(type) &&
            String.IsNullOrEmpty(subType))
        {
            bimOriginated = false;
        }

        // Retrieve and set the BIM Id
        // bimDef.BIMID = GetBIMId(go);



        // Set the BIM Definition and Properties on the Game Object
        string tagName = null;

        ///////////////////////////////////////////////////////////////
        // 3rd component
        // Update the GameObject name
        if (bimOriginated)
        {
            ///////////////////////////////////////////////////////////////
            // 1st component
            BIMDefinition bimDef = go.AddComponent<BIMDefinition>();

            bimDef.Category = category;
            bimDef.Type = type;
            bimDef.SubType = subType;

            string objectName = GetName(go,
                                        bimDef);

            go.name = objectName; //look into tag problem

            BIMRelationship bimInformation = go.AddComponent<BIMRelationship>();

            // Set the BIM ID
            // JL: REFACTOR - ONLY SET BIM NAME IF BIM DEFINITION - ie. generated in BIM tool
            string bimID = GetBIMId(go);
            bimInformation.BIMID = bimID;

            // bimInformation.BIMProperties = bimProperties.ToArray();
            tagName = bimDef.Category;
        }
        else
        {
            tagName = importConfig.DefaultTag;
        }

        ///////////////////////////////////////////////////////////////
        // 2nd component
        // Set the BIM Information and BIM Properties
        // bimInformation.bimDefinition = bimDef;
        BIMProperties bimProps = go.AddComponent<BIMProperties>();
        bimProps.Properties = bimProperties.ToArray();

        // Add the tag
        TagManager tagManager = TagManager.getInstance();
        // JL: category may not be defined
        tagManager.AddTag(tagName);

        go.tag = tagName;        // Add the tag
    }

    // Initial Function
    void OnPreprocessModel()
    {
        m_childObjects = new List<GameObject>();
        m_parentsCreated = new List<GameObject>();
        
    }

    // Final Function
	void OnPostprocessModel(GameObject g)
	{
        // Behaviour for doors
        // JL: TEMP
        m_mainParent = g;
        GameObject t_tempGO = new GameObject("Test");
        t_tempGO.transform.parent = m_mainParent.transform;

        

        PostprocessDoors (g);
		PostprocessorMeshColliders (g);
		//PostProcessLighting (g);
        PostProcessLayers(g);

        GroupObjects();
    }

    void GroupObjects()
    {
        Debug.Log("Grouping Objects");
        foreach (Transform child in m_mainParent.transform)
        {
            if (child.gameObject.GetComponent<BIMDefinition>())
            {
                BIMDefinition t_bimDefScript = child.gameObject.GetComponent<BIMDefinition>();
                CheckForParent(child.gameObject, t_bimDefScript);
            }
            
        }
    }

    void CheckForParent(GameObject _child, BIMDefinition _bimDefScript)
    {
        Debug.Log("Checking For Parent");
        Debug.Log("Parent Count: " + m_parentsCreated.Count);
        string t_parentsName = "Default";
        if (m_parentsCreated.Count <=0)
        {
            if (m_sortByCat)
            {
                t_parentsName = _bimDefScript.Category;
                CreateParent(_child, t_parentsName);
            }
            if (m_sortByType)
            {
                t_parentsName = _bimDefScript.Type;
                CreateParent(_child, t_parentsName);
            }
            if (m_sortBySubType)
            {
                t_parentsName = _bimDefScript.SubType;
                CreateParent(_child, t_parentsName);
            }
        }
        if (m_parentsCreated.Count > 0 && m_parentsCreated.Count < 30)
        {
            int t_int = m_parentsCreated.Count + 1;
            Debug.Log("Attempting to create parent: " + t_int);
            for (int i = 0; i < m_parentsCreated.Count; i++)
            {
                if (m_parentsCreated[i].name.Contains(_bimDefScript.SubType))
                {
                    Debug.Log("Found a parent, adding child to parent now");
                    AddChildToParent(m_parentsCreated[i], _child);
                }
                if (i == m_parentsCreated.Count - 1 && !m_parentsCreated[i].name.Contains(_bimDefScript.SubType))
                {
                    Debug.Log("Didnt find a parent, creating new one now");
                    t_parentsName = _bimDefScript.SubType;
                    CreateParent(_child, t_parentsName);
                }
            }
        }
        //else
        //{
        //    bool foundParent = false;
        //    for (int i = 0; i < m_parentsCreated.Count; i++)
        //    {
        //        if (m_sortByCat && (m_parentsCreated[i].name == _bimDefScript.Category))
        //        {
        //            AddChildToParent(m_parentsCreated[i], _child);
        //            foundParent = true;
        //        }
        //        if (m_sortByType && (m_parentsCreated[i].name == _bimDefScript.Type))
        //        {
        //            AddChildToParent(m_parentsCreated[i], _child);
        //            foundParent = true;
        //        }
        //        if (m_sortBySubType && (m_parentsCreated[i].name == _bimDefScript.SubType))
        //        {
        //            AddChildToParent(m_parentsCreated[i], _child);
        //            foundParent = true;
        //        }

        //        // if havent found parent create new one
        //        if (!foundParent && i == m_parentsCreated.Count - 1)
        //        {
        //            if (m_sortByCat)
        //            {
        //                t_parentsName = _bimDefScript.Category;
        //                CreateParent(_child, t_parentsName);
        //            }
        //            if (m_sortByType)
        //            {
        //                t_parentsName = _bimDefScript.Type;
        //                CreateParent(_child, t_parentsName);
        //            }
        //            if (m_sortBySubType)
        //            {
        //                t_parentsName = _bimDefScript.SubType;
        //                CreateParent(_child, t_parentsName);
        //            }
        //        }
        //    }
        //}
    }

    void CreateParent(GameObject _child, string _parentsName)
    {
        Debug.Log("Creating Parent");
        GameObject t_newParent = new GameObject(_parentsName);
        t_newParent.transform.parent = m_mainParent.transform;
        Debug.Log("Parent: " + t_newParent.name);
        AddChildToParent(t_newParent, _child);
        m_parentsCreated.Add(t_newParent);
    }

    void AddChildToParent(GameObject _parent, GameObject _child)
    {
        _child.transform.parent = _parent.transform;
    }

    private void PostProcessLayers(GameObject g)
    {
        // Assign the objects with BIM metadata to the layer
        ArchVizMetaInfo[] metadata = GameObject.FindObjectsOfType(typeof(ArchVizMetaInfo)) as ArchVizMetaInfo[];
        LayerMask archVizLayer = LayerMask.NameToLayer("ArchViz_BIM");

        foreach (ArchVizMetaInfo metaInfo in metadata)
        {
            metaInfo.gameObject.layer = archVizLayer;
        }
    }

	private void ProcessChildDoor(Transform currentTransform, bool bFoundDoorTag)
	{

		if (currentTransform.gameObject.tag.Equals ("Doors")) 
		{
			if (bFoundDoorTag) 
			{
				// Reset as already have a tag at a higher level
				currentTransform.gameObject.tag = "Untagged";
			} 
			else 
			{
				bFoundDoorTag = true;
			}
		}
		else
		{
			// Check for door in name
			string objectName = currentTransform.name;
			if ((objectName.Contains ("door")) || (objectName.Contains ("Door")) || objectName.Equals("Object623")  || objectName.Equals("Object622")  || objectName.Equals("Object614")  || objectName.Equals("Object615"))
			{
                currentTransform.gameObject.tag = "Doors";
				bFoundDoorTag = true;
			}
		}

		foreach (Transform childTransform in currentTransform) 
		{
			ProcessChildDoor(childTransform, bFoundDoorTag);
		}
	}


	private void PostprocessDoors(GameObject root)
	{
		// Hierarchies of door objects
		// If name contains door then check parent. If parent contains door then skip this current game object.
		// If parent does not contain door - determine if children has a door tag, if they do then remove the door tag
		// Set the door tag on this game object

		// EXAMPLE 1
		// - right door             [TAGGED] [SPHERICAL COLLIDER TRIGGER] [MESH COLLIDER]
		// -- Mesh3702				[MESH COLLIDER]
		// -- Object595				[MESH COLLIDER]
		// -- Object609				[MESH COLLIDER]
		// -- Object610				[MESH COLLIDER]

		// - door with glass botch	[TAGGED] [SPHERICAL COLLIDER TRIGGER ]
		// -- Doors:Door_Type D-10:D-10:851719 1 - PREVIOUSLY TAGGED AS DOOR
		// -- Object3073			[MESH COLLIDER]
		// -- Object3074			[MESH COLLIDER]
		// -- Object3075			[MESH COLLIDER]
		// -- Object3076			[MESH COLLIDER]
		// -- Object3077			[MESH COLLIDER]

		// - Door GF main	[TAGGED] [SPHERICAL COLLIDER TRIGGER] [MESH COLLIDER]


		// Retrieve all children of the root node
		foreach (Transform child in root.transform) 
		{
			// Retrieve the game object tag
			bool bFoundDoorTag = false;

			ProcessChildDoor(child, bFoundDoorTag);
		}


		// Add collidors
		// Behaviour for doors
		GameObject[] doors = GameObject.FindGameObjectsWithTag ("Doors");
		
		// Iteratre through the doors and create colliders
		foreach (GameObject door in doors) 
		{
			// Add a sphere collider to the Game Object
			SphereCollider sphereCollider = door.AddComponent<SphereCollider> ();
			sphereCollider.isTrigger = true;
			
			//Debug.Log ("* DOOR = " + door.name + " ***JL TEST*** COLLIDER" + sphereCollider.radius + " NAMED " + sphereCollider.name);
			// Debug.Log ("*** Collider Added with radius: " + sphereCollider.radius);
			
			// Add the Collider script
			// ArchVizTriggerCollider triggerCollider = 
			door.AddComponent<ArchVizTriggerCollider>();
		}
	}

	private void PostprocessorMeshColliders(GameObject root) 
	{
		GameObject[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

		foreach (GameObject gameObject in gameObjects) 
		{
			//if (!gameObject.CompareTag ("Doors"))
			//{
				// If not doors then add mesh collider
				MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
				// JL: INVESTIGATE: Mesh Collider. Convex.
				// Cannot have more than 256
				// meshCollider.convex = true;
			//}
		}
	}

	private void PostProcessLighting(GameObject root)
	{
		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Generic Models");

		// Load the generic lights materialModMaterials/
		Material myMaterial = Resources.Load("JL_LIGHTING", typeof(Material)) as Material;
		// F:\Unity\Projects\Arch Viz\Assets\Models\Architectural\Materials

		// Generic Models:WhiteCroft Lighting:Projection Screen
		foreach (GameObject gameObject in lights)
		{
			ArchVizMetaInfo archVizMetaInfo = gameObject.GetComponent<ArchVizMetaInfo>();

			if ((archVizMetaInfo != null) &
				(archVizMetaInfo.type.CompareTo ("WhiteCroft Lighting")==0) &
				(archVizMetaInfo.subType.CompareTo ("Projection Screen")==0))
			 {
				// Generic Models:Pavement Light:Pavement Light

				// Exchange the materials
				// Material currentMaterial = gameObject.Renderer.Material;
				Renderer renderer = gameObject.GetComponent<Renderer>();
				renderer.material = myMaterial;

				// gameObject.renderer. = myMaterial;

					// materials[0] = myMaterial;

					// .material = myMaterial;

				//Debug.Log("JL: SETUP MATERIAL LIGHT");
			 }

		}
	}

}
