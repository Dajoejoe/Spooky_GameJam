using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    bool collected;
    bool isHighlighted;
    MeshRenderer[] _renderers;
    Color originalColor;
    // Use this for initialization
    void Start()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
        originalColor = _renderers[0].material.color;
    }
    
	// Update is called once per frame
	void Update () {
		
	}

    public void Highlight(bool highlight)
    {
        if (isHighlighted == highlight)
        {
            return;
        }
        if (collected)
        {
            highlight = false;
        }

        isHighlighted = highlight;

        if (highlight)
        {
            foreach (var renderer in _renderers)
            {
                renderer.enabled = true;
                renderer.material.color = Color.green;
            }
        }
        else
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = originalColor;
            }
        }
    }

    public void CollectKey()
    {
        collected = true;
        gameObject.SetActive(false);
    }
}
