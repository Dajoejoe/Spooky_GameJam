using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public bool isActive;
    public int maxHealth = 10;
    public bool isBroken;

    int currentHealth;
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}

    // Update is called once per frame
    void Update() {

    }

    public bool Damage(int amt)
    {
        AdjustHealth(-amt);
        return currentHealth <= 0;
    }

    public bool Repair(int amt)
    {
        AdjustHealth(amt);
        return amt == maxHealth;
    }

    private void AdjustHealth(int amt)
    {
        currentHealth = Mathf.Clamp(currentHealth - amt, 0, maxHealth);
    }
}
