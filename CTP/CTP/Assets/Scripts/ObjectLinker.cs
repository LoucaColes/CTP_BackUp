using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ObjectLinker : MonoBehaviour {

    public GameObject[] m_multiplayerPrefabs;
    public GameObject m_parentObject;
    private GameObject m_currentPrefab;
    private List<GameObject> m_childObjects;
    private List<GameObject> m_childObjectsWithBimDef;
    private int m_numChildren;
    private int m_prefabId;
    private int m_prefabsCreatedCount;
    private string m_stringToCompare;
    public string m_filename;
    private bool m_useCat, m_useType, m_useSubType;
    private bool m_createdFolder;

    public void Init()
    {
        m_prefabId = 0;
        m_prefabsCreatedCount = 0;
        m_createdFolder = CheckIfFolderCreated();
        print("folder is created: " + m_createdFolder);
        if (!m_createdFolder)
        {
            print("creating new folder");
            CreateNewFolder();
        }
        m_currentPrefab = m_multiplayerPrefabs[m_prefabId];
        m_childObjects = new List<GameObject>();
        m_childObjectsWithBimDef = new List<GameObject>();
        m_numChildren = m_parentObject.transform.childCount;
        for (int i = 0; i < m_numChildren; i++)
        {
            m_childObjects.Add(m_parentObject.transform.GetChild(i).gameObject);
        }
        for (int j = 0; j < m_numChildren; j++)
        {
            if (m_childObjects[j].GetComponent<BIMDefinition>())
            {
                m_childObjectsWithBimDef.Add(m_childObjects[j]);
            }
        }
    }

    public void LinkObject()
    {
        if (m_stringToCompare != null)
        {
            for (int i = 0; i < m_childObjectsWithBimDef.Count; i++)
            {
                if (m_useSubType)
                {
                    LinkViaSubType(m_childObjectsWithBimDef[i]);
                }
                if (m_useType)
                {
                    LinkViaType(m_childObjectsWithBimDef[i]);
                }
                if (m_useCat)
                {
                    LinkViaCatergory(m_childObjectsWithBimDef[i]);
                }
            }
        }
    }

    void LinkViaSubType(GameObject _childToLink)
    {
        string t_objectsSubType = _childToLink.GetComponent<BIMDefinition>().SubType;
        if (m_stringToCompare.Equals(t_objectsSubType))
        {
            GameObject t_newObject = (GameObject)Instantiate(m_currentPrefab, _childToLink.transform.position, Quaternion.identity);
            t_newObject.transform.parent = _childToLink.transform;
        }
    }

    void LinkViaType(GameObject _childToLink)
    {
        string t_objectsType = _childToLink.GetComponent<BIMDefinition>().Type;
        if (m_stringToCompare.Equals(t_objectsType))
        {
            Instantiate(m_currentPrefab, _childToLink.transform.position, Quaternion.identity);
        }
    }

    void LinkViaCatergory(GameObject _childToLink)
    {
        string t_objectsCat = _childToLink.GetComponent<BIMDefinition>().Category;
        if (m_stringToCompare.Equals(t_objectsCat))
        {
            Instantiate(m_currentPrefab, _childToLink.transform.position, Quaternion.identity);
        }
    }

    public void SetStringToCompare(string _newString)
    {
        m_stringToCompare = _newString;
    }

    public void NextPrefab()
    {
        IncreasePrefabId();
        m_currentPrefab = m_multiplayerPrefabs[m_prefabId];
        print("current prefab id: " + m_prefabId);
    }

    void IncreasePrefabId()
    {
        m_prefabId++;
        if (m_prefabId > m_multiplayerPrefabs.Length)
        {
            m_prefabId = 0;
        }
    }

    public bool GetUseCatergory() { return m_useCat; }

    public void SetUseCatergory(bool _newValue)
    {
        m_useCat = _newValue;
        if (m_useCat)
        {
            m_useType = false;
            m_useSubType = false;
        }
    }

    public bool GetUseType() { return m_useType; }

    public void SetUseType(bool _newValue)
    {
        m_useType = _newValue;
        if (m_useType)
        {
            m_useCat = false;
            m_useSubType = false;
        }
    }

    public bool GetUseSubType() { return m_useSubType; }

    public void SetUseSubType(bool _newValue)
    {
        m_useSubType = _newValue;
        if (m_useSubType)
        {
            m_useCat = false;
            m_useType = false;
        }
    }

    bool CheckIfFolderCreated()
    {
        bool isCreated = AssetDatabase.IsValidFolder("Assets/Prefabs/ObjectLinkerPrefabs");
        return isCreated;
    }

    void CreateNewFolder()
    {
        string guid = AssetDatabase.CreateFolder("Assets/Prefabs", "ObjectLinkerPrefabs");
        string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
        print("Path of folder: " + newFolderPath);
    }

    public void SaveAsNewPrefab()
    {
        print("Saving a new prefabs");
        if (m_createdFolder && m_filename != "")
        {
            print("Saving a new prefabs");
            string t_filePath = "Assets/Prefabs/ObjectLinkerPrefabs/" + m_filename + m_prefabsCreatedCount + ".prefab";
            PrefabUtility.CreatePrefab(t_filePath, m_parentObject);
            m_prefabsCreatedCount++;
        }
    }
}
