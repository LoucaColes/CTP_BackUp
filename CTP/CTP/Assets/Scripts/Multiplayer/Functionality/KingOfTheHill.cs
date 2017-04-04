using UnityEngine;
using System.Collections;

public class KingOfTheHill : MonoBehaviour {

    [SerializeField]
    private GameObject m_king;
    [SerializeField]
    private int m_playersInHill;
    [SerializeField]
    private bool m_contested;
    private string m_currentTeamHolding;
    public GameObject m_base, m_pole;
    private Renderer m_baseRend, m_poleRend;

    // Use this for initialization
    void Start () {
        m_playersInHill = 0;
        m_contested = false;
        m_currentTeamHolding = "";
        m_baseRend = m_base.GetComponent<Renderer>();
        m_poleRend = m_pole.GetComponent<Renderer>();
        m_baseRend.material.color = Color.white;
        m_poleRend.material.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_playersInHill++;
            if (m_king == null)
            {
                m_king = other.gameObject;
                if (other.GetComponent<LocalPlayer>().m_team == "Blue")
                {
                    m_currentTeamHolding = "Blue";
                    m_baseRend.material.color = Color.blue;
                    m_poleRend.material.color = Color.blue;
                }
                if (other.GetComponent<LocalPlayer>().m_team == "Red")
                {
                    m_currentTeamHolding = "Red";
                    m_baseRend.material.color = Color.red;
                    m_poleRend.material.color = Color.red;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (m_playersInHill == 1)
            {
                if (m_king == null)
                {
                    m_king = other.gameObject;
                    if (other.GetComponent<LocalPlayer>().m_team == "Blue" && m_currentTeamHolding == "Red")
                    {
                        m_currentTeamHolding = "Blue";
                        m_baseRend.material.color = Color.blue;
                        m_poleRend.material.color = Color.blue;
                    }
                    if (other.GetComponent<LocalPlayer>().m_team == "Red" && m_currentTeamHolding == "Blue")
                    {
                        m_currentTeamHolding = "Red";
                        m_baseRend.material.color = Color.red;
                        m_poleRend.material.color = Color.red;
                    }
                }
                m_contested = false;
            }
            if (m_playersInHill > 1)
            {
                if (other.GetComponent<LocalPlayer>().m_team == "Blue" && m_currentTeamHolding == "Red")
                {
                    m_contested = true;
                }
                if (other.GetComponent<LocalPlayer>().m_team == "Red" && m_currentTeamHolding == "Blue")
                {
                    m_contested = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_playersInHill--;
            if (other.gameObject == m_king)
            {
                m_king = null;
                m_baseRend.material.color = Color.white;
                m_poleRend.material.color = Color.white;
            }
        }
    }
}
