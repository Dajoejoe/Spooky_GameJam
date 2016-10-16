using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public bool isActive = true;
    public int maxHealth = 10;
    public bool isBroken;
    public Window window;
    public Door door;

    int currentHealth;
    bool isHighlighted;
    MeshRenderer _renderer;
    Color originalColor;
    ParticleSystem ps;
    // Use this for initialization
    void Awake()
    {
        isActive = true;
    }
    
    void Start () {
        currentHealth = maxHealth;
        _renderer = GetComponent<MeshRenderer>();
        originalColor = _renderer.material.color;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {

    }

    public bool Damage(int amt)
    {
        if (isBroken) 
            return true;
        ps.Play();
        AdjustHealth(-amt);
        if (currentHealth <= 0)
        {
            isBroken = true;
            GetComponent<Renderer>().enabled = false;
            if (window)
            {
                window.CheckIfDead();
            }
        }
        return currentHealth <= 0;
    }

    public bool Repair(int amt)
    {
        if (currentHealth == maxHealth)
            return true;
        AdjustHealth(amt);
        if (currentHealth == maxHealth && isBroken)
        {
            isBroken = false;
            GetComponent<Renderer>().enabled = true;
        }
        return currentHealth == maxHealth;
    }

    private void AdjustHealth(int amt)
    {
        currentHealth = Mathf.Clamp(currentHealth + amt, 0, maxHealth);
    }

    public void Highlight(bool highlight)
    {
        if (isHighlighted == highlight)
        {
            return;
        }
        if (door != null && door.isDead)
        {
            _renderer.enabled = false;
            return;
        }
        if (window != null && window.isDead)
        {
            _renderer.enabled = false;
            return;
        }

        isHighlighted = highlight;

        if (highlight)
        {
            _renderer.enabled = true;
            _renderer.material.color = Color.green;
        }
        else
        {
            if (isBroken)
            {
                _renderer.enabled = false;
            }
            _renderer.material.color = originalColor;
        }
    }
}
