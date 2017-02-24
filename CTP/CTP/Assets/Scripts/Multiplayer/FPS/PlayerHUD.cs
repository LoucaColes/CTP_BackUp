﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHUD : NetworkBehaviour {

    public Text m_healthText;
    public Text m_magsText;
    public Text m_roundsText;
    private GameObject currentGun;
    private Gun gunScript;
    public Player player;
    [SyncVar(hook = "OnChangeHealth")]
    private int playerHealth;
    //[SyncVar(hook = "OnChangeHealth")]
    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        playerHealth = player.GetHealth();
        m_roundsText.text = "Rounds in Mag: " + gunScript.GetAmmoCount();
        m_magsText.text = "Mags left: " + gunScript.GetMagCount();
	}

    void OnChangeHealth(int playerHealth)
    {
        m_healthText.text = "Health: " + playerHealth;
    }

    public void SetGun(GameObject _currentGun)
    {
        currentGun = _currentGun;
        gunScript = currentGun.GetComponent<Gun>();
    }
}