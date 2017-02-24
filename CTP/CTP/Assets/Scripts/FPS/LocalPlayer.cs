using UnityEngine;
using System.Collections;

public class LocalPlayer : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;

    // Use this for initialization
    void Start()
    {

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
        }

    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
