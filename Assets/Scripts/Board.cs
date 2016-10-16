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
        ps.Play();
        AdjustHealth(-amt);
        Debug.Log("Damage: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("turn off renderer");
            isBroken = true;
            GetComponent<Renderer>().enabled = false;
        }
        return currentHealth <= 0;
    }

    public bool Repair(int amt)
    {
        AdjustHealth(amt);
        Debug.Log("Repair: " + currentHealth);
        if (amt == maxHealth && isBroken)
        {
            isBroken = false;
            GetComponent<Renderer>().enabled = true;
        }
        return amt == maxHealth;
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
