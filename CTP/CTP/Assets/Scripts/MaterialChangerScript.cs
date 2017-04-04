using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MaterialChangerScript : MonoBehaviour {

    private bool m_UseTags, m_UseString, m_UseSelection;
    private bool m_UseCat, m_UseType, m_UseSubType;
    public string m_tag;
    public string[] m_strings, m_catStrings, m_typeStrings, m_subTypeStrings;
    public int[] m_matIDs;
    public GameObject[] m_selection;
    [SerializeField]private List<GameObject> m_objectsToChange;
    public Material m_newMaterial;
    private int m_prefabsCreatedCount;
    public string m_filename;
    private bool m_createdFolder;
    public GameObject m_gameObjectToSave;

    // Use this for initialization
    void Start () {
        m_objectsToChange = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
	    
    }

    public bool GetUseTags() { return m_UseTags; }

    public void SetUseTags(bool _newValue)
    {
        m_UseTags = _newValue;
        if (m_UseTags)
        {
            m_UseString = false;
            m_UseSelection = false;
            m_UseCat = false;
            m_UseType = false;
            m_UseSubType = false;
        }
    }

    public bool GetUseString() { return m_UseString; }

    public void SetUseString(bool _newValue)
    {
        m_UseString = _newValue;
        if (m_UseString)
        {
            m_UseTags = false;
            m_UseSelection = false;
            m_UseCat = false;
            m_UseType = false;
            m_UseSubType = false;
        }
    }

    public bool GetUseSelection() { return m_UseSelection; }

    public void SetUseSelection(bool _newValue)
    {
        m_UseSelection = _newValue;
        if (m_UseSelection)
        {
            m_UseTags = false;
            m_UseString = false;
            m_UseCat = false;
            m_UseType = false;
            m_UseSubType = false;
        }
    }

    public bool GetUseCatergory() { return m_UseCat; }

    public void SetUseCatergory(bool _newValue)
    {
        m_UseCat = _newValue;
        if (m_UseCat)
        {
            m_UseTags = false;
            m_UseString = false;
            m_UseSelection = false;
            m_UseType = false;
            m_UseSubType = false;
        }
    }

    public bool GetUseType() { return m_UseType; }

    public void SetUseType(bool _newValue)
    {
        m_UseType = _newValue;
        if (m_UseType)
        {
            m_UseTags = false;
            m_UseString = false;
            m_UseCat = false;
            m_UseSelection = false;
            m_UseSubType = false;
        }
    }

    public bool GetUseSubType() { return m_UseSubType; }

    public void SetUseSubType(bool _newValue)
    {
        m_UseSubType = _newValue;
        if (m_UseSubType)
        {
            m_UseTags = false;
            m_UseString = false;
            m_UseCat = false;
            m_UseType = false;
            m_UseSelection = false;
        }
    }

    public void GetObjects()
    {
        if (!m_UseTags && !m_UseString && !m_UseSelection && !m_UseCat && !m_UseType && !m_UseSubType)
        {
            Debug.LogError("Please select an option of how to find objects!");
        }
        if (m_UseSelection)
        {
            GetViaSelection();
        }
        if (m_UseString || m_UseTags || m_UseCat || m_UseType || m_UseSubType)
        {
            GameObject[] m_allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            for (int i = 0; i < m_allObjects.Length; i++)
            {
                if (m_UseTags)
                {
                    GetViaTags(m_allObjects[i]);
                }
                if (m_UseString)
                {
                    GetViaString(m_allObjects[i]);
                }
                if (m_UseCat)
                {
                    GetViaCatergory(m_allObjects[i]);
                }
                if (m_UseType)
                {
                    GetViaType(m_allObjects[i]);
                }
                if (m_UseSubType)
                {
                    GetViaSubType(m_allObjects[i]);
                }
            }
        }
    }

    private void GetViaTags(GameObject _objectToCheck)
    {
        if (_objectToCheck.tag == m_tag)
        {
            m_objectsToChange.Add(_objectToCheck);
        }
    }

    private void GetViaString(GameObject _objectToCheck)
    {
        for (int i = 0; i < m_strings.Length; i++)
        {
            if (_objectToCheck.name.Contains(m_strings[i]))
            {
                m_objectsToChange.Add(_objectToCheck);
            }
        }
    }

    private void GetViaCatergory(GameObject _objectToCheck)
    {
        for (int i = 0; i < m_catStrings.Length; i++)
        {
            if (_objectToCheck.GetComponent<BIMDefinition>())
            {
                if (_objectToCheck.GetComponent<BIMDefinition>().Category.Contains(m_catStrings[i]))
                {
                    m_objectsToChange.Add(_objectToCheck);
                }
            }
        }
    }

    private void GetViaType(GameObject _objectToCheck)
    {
        for (int i = 0; i < m_typeStrings.Length; i++)
        {
            if (_objectToCheck.GetComponent<BIMDefinition>())
            {
                if (_objectToCheck.GetComponent<BIMDefinition>().Type.Contains(m_typeStrings[i]))
                {
                    m_objectsToChange.Add(_objectToCheck);
                }
            }
        }
    }

    private void GetViaSubType(GameObject _objectToCheck)
    {
        for (int i = 0; i < m_subTypeStrings.Length; i++)
        {
            if (_objectToCheck.GetComponent<BIMDefinition>())
            {
                if (_objectToCheck.GetComponent<BIMDefinition>().SubType.Contains(m_subTypeStrings[i]))
                {
                    m_objectsToChange.Add(_objectToCheck);
                }
            }
        }
    }

    private void GetViaSelection()
    {
        for (int i = 0; i < m_selection.Length; i++)
        {
            m_objectsToChange.Add(m_selection[i]);
        }
    }

    public void ClearList()
    {
        m_objectsToChange.Clear();
    }

    public void ChangeMaterials()
    {
        if (m_objectsToChange.Count > 0)
        {
            Renderer t_renderer;

            for (int i = 0; i < m_objectsToChange.Count; i++)
            {
                if (m_objectsToChange[i].GetComponent<Renderer>())
                {
                    t_renderer = m_objectsToChange[i].GetComponent<Renderer>();
                    if (t_renderer.materials.Length == 1)
                    {
                        t_renderer.material = m_newMaterial;
                    }
                    else if (t_renderer.materials.Length > 1)
                    {
                        Material[] t_mats = t_renderer.materials;
                        for (int j = 0; j < t_renderer.materials.Length; j++)
                        {
                            if (m_matIDs.Length > 0)
                            {
                                for (int k = 0; k < m_matIDs.Length; k++)
                                {
                                    if (j == m_matIDs[k])
                                    {
                                        t_mats[j] = m_newMaterial;
                                        t_renderer.materials = t_mats;
                                    }
                                }
                            }
                        }
                    }
                    t_renderer = null;
                }
            }
        }
    }

    bool CheckIfFolderCreated()
    {
        bool isCreated = AssetDatabase.IsValidFolder("Assets/Prefabs/MaterialChangedPrefabs");
        return isCreated;
    }

    void CreateNewFolder()
    {
        string guid = AssetDatabase.CreateFolder("Assets/Prefabs", "MaterialChangedPrefabs");
        string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
        print("Path of folder: " + newFolderPath);
    }

    public void SaveAsNewPrefab()
    {
        m_createdFolder = CheckIfFolderCreated();
        print("folder is created: " + m_createdFolder);
        if (!m_createdFolder)
        {
            print("creating new folder");
            CreateNewFolder();
        }
        print("Saving a new prefabs");
        if (m_createdFolder && m_filename != "")
        {
            print("Saving a new prefabs");
            string t_filePath = "Assets/Prefabs/MaterialChangedPrefabs/" + m_filename + m_prefabsCreatedCount + ".prefab";
            PrefabUtility.CreatePrefab(t_filePath, m_gameObjectToSave);
            m_prefabsCreatedCount++;
        }
    }
}
