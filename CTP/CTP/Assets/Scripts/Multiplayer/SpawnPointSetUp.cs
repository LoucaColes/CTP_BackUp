using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpawnPointSetUp : MonoBehaviour {

    private NetworkLobbyManager m_networkLobbyManagerScript;
    private GameObject[] m_spawnPoints;
    private bool m_hasSpawnPoints;

	// Use this for initialization
	void Start () {
        m_networkLobbyManagerScript = gameObject.GetComponent<NetworkLobbyManager>();
        m_hasSpawnPoints = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == m_networkLobbyManagerScript.playScene)
        {
            if (!m_hasSpawnPoints)
            {
                m_spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
                m_hasSpawnPoints = true;
            }
        }
            
	}
}
