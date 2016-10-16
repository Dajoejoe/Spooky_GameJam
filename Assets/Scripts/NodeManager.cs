using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

    GameObject[] nodes;

	// Use this for initialization
	void Start () {
        nodes = GameObject.FindGameObjectsWithTag(Tags.Node);
        for (int i=0; i < nodes.Length; i++)
        {
            nodes[i].GetComponent<Node>().id = i + 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
