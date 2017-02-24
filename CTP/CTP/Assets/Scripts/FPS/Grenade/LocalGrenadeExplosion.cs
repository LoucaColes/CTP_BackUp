using UnityEngine;
using System.Collections;

public class LocalGrenadeExplosion : MonoBehaviour {

    [SerializeField]
    private float m_radius;
    [SerializeField]
    private float m_powerMultiplier;
    private float m_stopwatch;
    [SerializeField] private float m_fuse, m_particleTime;
    [SerializeField]
    private int m_playerDamage;
    public GameObject m_explosionParticle;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_stopwatch += Time.deltaTime;
        if (m_stopwatch > m_fuse)
        {
            print("Time Up");
            Explode();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, m_radius);
    }

    void Explode()
    {
        GetCollidersWithinRadius();
        SpawnParticle();
        Destroy(gameObject);
    }

    void SpawnParticle()
    {
        print("spawning particles");
        GameObject tempParticle = (GameObject)Instantiate(m_explosionParticle, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(tempParticle, m_particleTime);
    }

    void GetCollidersWithinRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //gets distance between grenade and object
            float m_dist = getDistance(hitColliders[i].transform.position);
            //gets inverted % of distance away
            //so edge of radius is 0% & center is 100% 
            float m_power = 100 - ((m_dist / m_radius) * 100);
            //pushes away based on power
            pushAway(m_power, hitColliders[i].gameObject);
            DealDamage(hitColliders[i].gameObject);
        }
    }

    private float getDistance(Vector3 _objectPos)
    {
        return Vector3.Distance(gameObject.transform.position, _objectPos);
    }

    private void pushAway(float _force, GameObject _object)
    {
        if (_object.GetComponent<Rigidbody>() != null)
        {
            Rigidbody m_rigidbody = _object.GetComponent<Rigidbody>();
            //calculate direction to push object
            Vector3 m_dir = (_object.transform.position - gameObject.transform.position).normalized;
            //adds force to object away from bomb by %power
            m_rigidbody.AddForce(m_dir * (_force * m_powerMultiplier));
        }

    }

    void DealDamage(GameObject _object)
    {
        if (_object.GetComponent<LocalPlayer>() != null)
        {
            _object.GetComponent<LocalPlayer>().TakeDamage(m_playerDamage);
        }
    }
}
