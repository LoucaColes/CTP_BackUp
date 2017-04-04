using UnityEngine;
using System.Collections;

public class LocalPlayer : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public string m_team;
    public int m_playerID;
    public GameObject visor;
    public GameObject m_spawnPoint;

    // Use this for initialization
    void Start()
    {
        gameObject.transform.position = m_spawnPoint.transform.position;
        if (m_team == "FreeForAll")
        {
            visor.GetComponent<Renderer>().material.color = Color.black;
        }
        if (m_team == "Blue")
        {
            visor.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (m_team == "Red")
        {
            visor.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
            gameObject.transform.position = m_spawnPoint.transform.position;
            currentHealth = maxHealth;
        }

    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
