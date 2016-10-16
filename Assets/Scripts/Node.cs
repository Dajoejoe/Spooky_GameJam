using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public int id;
    public GameObject[] connectionNodes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class NodeConnection
{
    public float distance;
    public GameObject node1;
    public GameObject node2;
}
