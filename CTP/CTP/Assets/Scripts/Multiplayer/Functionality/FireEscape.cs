using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireEscape : MonoBehaviour {

    public float m_timeToEscape, m_timeTillAlarm;
    private float m_timer, m_alarmTimer;
    public Text m_timerDisplayText;
    public Canvas m_timerDisplayCanvasPref;
    public GameObject m_tickingASGO, m_tickingASGOPref;
    private AudioSource m_tickingAS;
    public GameObject m_alarmASGO, m_alarmASGOPref;
    private AudioSource m_alarmAS;

    // Use this for initialization
    void Start () {
        m_timer = m_timeToEscape;
        m_alarmTimer = m_timeTillAlarm;
        if (m_timerDisplayText == null)
        {
            Canvas t_newCanvas = Instantiate(m_timerDisplayCanvasPref);
            m_timerDisplayText = t_newCanvas.gameObject.transform.GetChild(0).GetComponent<Text>();
        }
        if (m_tickingASGO == null)
        {
            GameObject t_newGO = (GameObject)Instantiate(m_tickingASGOPref, new Vector3(0,2,0), Quaternion.identity);
            m_tickingASGO = t_newGO;
            m_tickingAS = m_tickingASGO.GetComponent<AudioSource>();
        }
        if (m_tickingASGO != null)
        {
            m_tickingAS = m_tickingASGO.GetComponent<AudioSource>();
        }
        if (m_alarmASGO == null)
        {
            GameObject t_newGO = (GameObject)Instantiate(m_alarmASGOPref, new Vector3(0, 2, 0), Quaternion.identity);
            m_alarmASGO = t_newGO;
            m_alarmAS = m_alarmASGO.GetComponent<AudioSource>();
        }
        if (m_alarmASGO != null)
        {
            m_alarmAS = m_alarmASGO.GetComponent<AudioSource>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_alarmTimer -= Time.deltaTime;
        if (m_alarmTimer > 0)
        {
            m_timerDisplayText.text = "Time Till Alarm: " + (int)m_alarmTimer;
            
        }
        if (m_alarmTimer < 0)
        {
            if (m_alarmAS.mute)
            {
                m_alarmAS.mute = false;
            }
            print("Alarm is going off, get to the point");
            Destroy(m_tickingASGO);
            if (m_timer > 0)
            {
                m_timerDisplayText.text = "Time Left: " + (int)m_timer;
                m_timer -= Time.deltaTime;
            }
            if (m_timer < 0)
            {
                if (!m_alarmAS.mute)
                {
                    m_alarmAS.mute = true;
                }
                print("Time is Up");
                m_timerDisplayText.text = "Time Up";
            }
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && m_timer > 0)
        {
            print("Player has escaped");
        }
    }
}
