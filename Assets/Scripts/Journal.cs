using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour {

    bool haveRead = false;
    bool isHighlighted;
    MeshRenderer _renderer;
    Color originalColor;
    GameObject journal;
    // Use this for initialization
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        if (_renderer)
        { 
            originalColor = _renderer.material.color;
        }
        journal = GameObject.FindGameObjectWithTag(Tags.Journal);
        if (journal)
            journal.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (journal != null && journal.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            journal.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void Highlight(bool highlight)
    {
        if (isHighlighted == highlight)
        {
            return;
        }
        if (haveRead)
        {
            highlight = false;
        }

        isHighlighted = highlight;

        if (highlight)
        {
            _renderer.enabled = true;
            _renderer.material.color = Color.green;
        }
        else
        {
            _renderer.material.color = originalColor;
        }
    }

    public void ReadJournal()
    {
        journal.SetActive(true);
    }
}
