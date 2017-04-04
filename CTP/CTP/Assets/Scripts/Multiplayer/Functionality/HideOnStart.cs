using UnityEngine;
using System.Collections;

public class HideOnStart : MonoBehaviour {

    public bool m_hide = false;
    private Renderer m_baseRenderer, m_poleRenderer;
    private Collider m_baseCollider, m_poleCollider;
    private GameObject m_base, m_pole;

	// Use this for initialization
	void Start () {
        m_base = gameObject.transform.GetChild(0).gameObject;
        m_baseRenderer = m_base.GetComponent<Renderer>();
        m_baseCollider = m_base.GetComponent<Collider>();

        m_pole = m_base.transform.GetChild(0).gameObject;
        m_poleRenderer = m_pole.GetComponent<Renderer>();
        m_poleCollider = m_pole.GetComponent<Collider>();

        if (m_hide)
        {
            m_baseRenderer.enabled = false;
            m_baseCollider.enabled = false;
            m_poleRenderer.enabled = false;
            m_poleCollider.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetToHide()
    {
        m_hide = true;
    }
}
