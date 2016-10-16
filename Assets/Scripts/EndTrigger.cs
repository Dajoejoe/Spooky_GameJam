using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

    public int nextLevel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider colider)
    {
        Debug.Log("hit");
        Debug.Log(colider.gameObject.tag);
        if (colider.gameObject.tag == Tags.Player)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
