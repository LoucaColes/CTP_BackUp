using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialChangerScript : MonoBehaviour {

    private bool m_UseTags, m_UseString, m_UseSelection;
    public string m_tag;
    public string[] m_strings;
    public GameObject[] m_selection;
    [SerializeField]private List<GameObject> m_objectsToChange;
    public Material m_newMaterial;

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
        }
    }

    public void GetObjects()
    {
        if (!m_UseTags && !m_UseString && !m_UseSelection)
        {
            Debug.LogError("Please select an option of how to find objects!");
        }
        if (m_UseSelection)
        {
            GetViaSelection();
        }
        if (m_UseString || m_UseTags)
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
                    t_renderer.material = m_newMaterial;
                    t_renderer = null;
                }
            }
        }
    }
}
