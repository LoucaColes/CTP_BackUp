using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSorter : MonoBehaviour {

    public string[] m_strings, m_tags;
    [SerializeField] private List<GameObject> m_childObjects;
    [SerializeField] private List<GameObject> m_parentsCreated;
    private bool m_ranInit = false;
    private GameObject m_mainParent;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init()
    {
        m_childObjects = new List<GameObject>();
        m_parentsCreated = new List<GameObject>();
        m_mainParent = gameObject;
        m_ranInit = true;
    }

    public void GetChildObjects()
    {
        int m_childCount = gameObject.transform.childCount;
        if (!m_ranInit)
        {
            Debug.LogError("Please Run Init");
        }
        else
        {
            for (int i = 0; i < m_childCount; i++)
            {
                m_childObjects.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void SortByTags()
    {
        if (m_childObjects.Count > 0)
        {
            for (int i = 0; i < m_childObjects.Count; i++)
            {
                for (int j = 0; j < m_tags.Length; j++)
                {
                    print("objectTag: " + m_childObjects[i].tag);
                    print("tag: " + m_tags[j]);
                    if (m_childObjects[i].tag == m_tags[j])
                    {
                        //if (m_parentsCreated.Count > 0)
                        //{
                        //    for (int k = 0; k < m_parentsCreated.Count; k++)
                        //    {
                        //        if (m_parentsCreated[k].name == _parentName)
                        //        {
                        //            parentFound = true;
                        //        }
                        //        else
                        //        {
                        //            parentFound = false;
                        //        }
                        //    }
                        //}
                        if (!CheckForParent(m_tags[j]))
                        {
                            print("didnt find parent");
                            CreateNewParent(m_tags[j]);
                            AddToParent(m_childObjects[i], m_tags[j]);
                        }
                        else
                        {
                            print("found parent");
                            AddToParent(m_childObjects[i], m_tags[j]);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Please Get Child Objects First");
        }
    }

    public void Clear()
    {
        m_childObjects.Clear();
        m_parentsCreated.Clear();
    }

    private void CreateNewParent(string _parentName)
    {
        GameObject newParent = new GameObject();
        newParent.name = _parentName;
        newParent.transform.parent = m_mainParent.transform;
        m_parentsCreated.Add(newParent);
    }

    private bool CheckForParent(string _parentName)
    {
        bool parentFound = false;
        if (m_parentsCreated.Count > 0)
        {
            for (int i = 0; i < m_parentsCreated.Count; i++)
            {
                if (m_parentsCreated[i].name == _parentName)
                {
                    parentFound = true;
                }
                else
                {
                    parentFound = false;
                }
            }
        }
        else
        {
            parentFound = false;
        }
        return parentFound;
    }

    private int FindParent(string _parentName)
    {
        int parentID = -1;
        for (int i = 0; i < m_parentsCreated.Count; i++)
        {
            if (m_parentsCreated[i].name == _parentName)
            {
                parentID = i;
            }
        }
        return parentID;
    }

    private void AddToParent(GameObject _objectToAdd, string _parentName)
    {
        int parentID = FindParent(_parentName);
        _objectToAdd.transform.parent = m_parentsCreated[parentID].transform;
    }
}
