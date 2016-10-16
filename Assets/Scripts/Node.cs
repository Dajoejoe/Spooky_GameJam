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
    public Node node1;
    public Node node2;

    public NodeConnection(Node n1, Node n2)
    {
        node1 = n1;
        node2 = n2;
        distance = Vector3.Distance(n1.transform.position, n1.transform.position);
    }

    public bool Equals(int id1, int id2)
    {
        var n1 = node1.GetComponent<Node>().id;
        var n2 = node2.GetComponent<Node>().id;

        return (id1 == n1 && id2 == n2) || (id1 == n2 && id2 == n1);
    }

    public Node OtherNode(Node n1)
    {
        if (n1 == node1)
            return node2;
        else
            return node1;
    }

    public bool ContainsNode(Node n)
    {
        return n == node1 || n == node2;
    }
}
