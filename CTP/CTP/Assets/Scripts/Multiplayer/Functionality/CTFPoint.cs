using UnityEngine;
using System.Collections;

public class CTFPoint : MonoBehaviour {

    public GameObject m_flagPrefab;
    private GameObject m_flag;
    public string m_team;
    private bool m_hasFlag;

	// Use this for initialization
	void Start () {
        m_hasFlag = true;
        m_flag = (GameObject)Instantiate(m_flagPrefab, gameObject.transform.position, Quaternion.identity);
        m_flag.transform.parent = gameObject.transform;
        m_flag.GetComponent<CTFFlag>().SetFlagPoint(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<LocalPlayer>().m_team == m_team)
            {
                if (other.gameObject.transform.GetChild(3))
                {
                    other.gameObject.transform.GetChild(3).GetComponent<CTFFlag>().ResetFlag();
                    print("Player scored!");
                }
            }
            if (other.gameObject.GetComponent<LocalPlayer>().m_team != m_team)
            {
                if (m_hasFlag)
                {
                    m_flag.transform.position = other.gameObject.transform.position;
                    m_flag.transform.parent = other.gameObject.transform;
                }
            }
        }
    }
}
