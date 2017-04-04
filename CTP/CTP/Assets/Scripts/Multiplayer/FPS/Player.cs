using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    private NetworkStartPosition[] spawnPoints;
    public bool destroyOnDeath = false;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(50);
        }
	}

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, invoked on the Clients
                RpcRespawn();
            }
        }

    }

    public int GetHealth()
    {
        return currentHealth;
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position + new Vector3(0f, 3f, 0f);
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
}
