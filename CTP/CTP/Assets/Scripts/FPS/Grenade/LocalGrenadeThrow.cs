using UnityEngine;
using System.Collections;

public class LocalGrenadeThrow : MonoBehaviour {

    public GameObject m_grenadePrefab;
    public Transform m_throwPoint;
    public float m_throwForce;
    private GameObject m_camera;
    private GameObject m_tempGrenade;

    // Use this for initialization
    void Start () {
	    m_camera = gameObject.transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnGrenade();
        }
        if (Input.GetKey(KeyCode.G))
        {
            m_tempGrenade.transform.position = m_throwPoint.position;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            ThrowGrenade();
        }
	}

    void SpawnGrenade()
    {
        m_tempGrenade = (GameObject)Instantiate(m_grenadePrefab, m_throwPoint.position, m_throwPoint.rotation);
        
        m_tempGrenade.GetComponent<Rigidbody>().isKinematic = true;
        m_tempGrenade.GetComponent<Rigidbody>().useGravity = false;
    }

    void ThrowGrenade()
    {
        m_tempGrenade.GetComponent<Rigidbody>().isKinematic = false;
        m_tempGrenade.GetComponent<Rigidbody>().useGravity = true;
        Vector3 m_tempDir = Vector3.forward;
        m_tempDir = m_camera.transform.rotation * m_tempDir;
        m_tempGrenade.GetComponent<Rigidbody>().AddForce(m_tempDir * m_throwForce);
        m_tempGrenade = null;
    }
}
