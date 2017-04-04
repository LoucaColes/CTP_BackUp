using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DemoDropdown : MonoBehaviour {

    public GameObject m_loadCanvas;
    public Dropdown m_dropdown;
    private int m_currentSceneId;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(m_loadCanvas);
        DontDestroyOnLoad(m_dropdown);
        m_currentSceneId = m_dropdown.value;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadDemoScene()
    {
        if (m_dropdown.value != Application.loadedLevel)
        {
            m_currentSceneId = m_dropdown.value;
            Application.LoadLevel(m_currentSceneId);
        }
    }

   
}
