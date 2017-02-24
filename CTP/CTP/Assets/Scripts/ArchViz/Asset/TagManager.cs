using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

#if UNITY_EDITOR
public class TagManager
{
    private static TagManager instance;

    List<string> tags; 

    /*
	// Use extension method.
	List<int> list = arrayList.ToList<int>();
	foreach (int value in list)
	{
	    Console.WriteLine(value);
	}
     * */ 

    public static TagManager getInstance()
    {
        if (instance == null)
        {
            instance = new TagManager();
            instance.tags = new List<string>();

            // Reconcile Tags with Unity Tag Manager
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagsProperty = tagManager.FindProperty("tags");

            for (int i = 0; i < tagsProperty.arraySize; i++)
            {
                SerializedProperty tagProp = tagsProperty.GetArrayElementAtIndex(i);
                instance.tags.Add(tagProp.stringValue);
            }
        }

        return instance;
    }

    private bool TagExists(string tagName)
    {
        foreach (string tag in tags)
        {
            if (tag.Equals(tagName))
                return true;
        }

        return false;
    }

    public void AddTag(string tagName)
    {
        // If tag does not exist
        if (!TagExists(tagName))
        {
            // Retrieve the Tag Manager
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagsProperty = tagManager.FindProperty("tags");

            // Add tag
            tagsProperty.InsertArrayElementAtIndex(0);
            SerializedProperty prop = tagsProperty.GetArrayElementAtIndex(0);
            prop.stringValue = tagName;

            // Save the changes
            tagManager.ApplyModifiedProperties();

            // Add to the cache
            instance.tags.Add(tagName);
        }

    }

    /*
    bool TagExists(SerializedProperty tagsProperty,
                   string tagName)
    {
        bool isFound = false;

        for (int i = 0; i < tagsProperty.arraySize; i++)
        {
            SerializedProperty tagProp = tagsProperty.GetArrayElementAtIndex(i);
            if (tagProp.stringValue.Equals(tagName))
            {
                isFound = true;
                break;
            }
        }

        return isFound;
    }
     * */
}

#endif