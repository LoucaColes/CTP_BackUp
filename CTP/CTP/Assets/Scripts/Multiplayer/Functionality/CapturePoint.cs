using UnityEngine;
using System.Collections;

public class CapturePoint : MonoBehaviour {

    public float m_captureTime;
    [SerializeField] private float m_timer;
    [SerializeField]
    private bool m_captured, m_capturing;
    [SerializeField]
    private int m_playersAtPoint;
    private bool m_contested;
    private string m_currentTeamHolding;
    public GameObject m_base, m_pole;
    private Renderer m_baseRend, m_poleRend;

    // Use this for initialization
    void Start () {
        m_captured = false;
        m_capturing = false;
        m_contested = false;
        m_currentTeamHolding = "";
        m_baseRend = m_base.GetComponent<Renderer>();
        m_poleRend = m_pole.GetComponent<Renderer>();
        m_baseRend.material.color = Color.white;
        m_poleRend.material.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {
	    if (m_capturing)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_captureTime)
            {
                m_baseRend.material.color = Color.yellow;
                m_poleRend.material.color = Color.yellow;
                m_captured = true;
                m_capturing = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_playersAtPoint++;
            if (!m_capturing)
            {
                m_capturing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_playersAtPoint--;
            if (m_playersAtPoint < 1 && m_capturing)
            {
                m_capturing = false;
                m_timer = 0f;
            }
        }
    }
}
