using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeManager : MonoBehaviour {

    GameObject[] nodes;

    List<NodeConnection> connections;

	// Use this for initialization
	void Start () {
        nodes = GameObject.FindGameObjectsWithTag(Tags.Node);
        for (int i=0; i < nodes.Length; i++)
        {
            nodes[i].GetComponent<Node>().id = i + 1;
        }
        CreateConnections();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateConnections()
    {
        connections = new List<NodeConnection>();
        foreach (var node in nodes)
        {
            var node1 = node.GetComponent<Node>(); 
            foreach (var connection in node.GetComponent<Node>().connectionNodes)
            {
                var node2 = connection.GetComponent<Node>();
                if (!connections.Any(x => x.Equals(node1.id, node2.id)))
                {
                    connections.Add(new NodeConnection(node1, node2));
                }
            }
        }
    }
}

public class PathFinder
{
    List<NodeConnection> connections;
    

    public PathFinder(List<NodeConnection> connections)
    {
        this.connections = connections;
    }

    //public List<NodeConnection> FindPath(Node from, Node to)
    //{
    //    List<List<NodeConnection>> paths = new List<List<NodeConnection>>();
    //    foreach (var connection in connections.Where(c => c.node1 == from || c.node2 == from))
    //    {
    //        var path = DistanceToConnection(from, connection, to, new List<NodeConnection>());
    //        if (path == null || path.Count < 2)
    //        {
    //            continue;
    //        }
    //        paths.Add(path);
    //    }
        
    //    float min = Mathf.Infinity;
    //    List<NodeConnection> shortest = null;
    //    foreach (var path in paths)
    //    {
    //        float distance = 0;
    //        foreach (var connection in path)
    //        {
    //            distance += connection.distance;
    //        }
    //        if (distance < min)
    //        {
    //            min = distance;
    //            shortest = path;
    //        }
    //    }
    //    return shortest;
    //}

    //private List<NodeConnection> DistanceToConnection(Node from, NodeConnection currentConnection, Node end)
    //{
    //    if (!connections.Any(c => c != currentConnection))
    //    {
    //        Debug.Log("Dead end");
    //        return null;
    //    }

    //    if (currentConnection.OtherNode(from) == end)
    //    {
    //        return path;
    //    }

    //    float min = Mathf.Infinity;

    //    foreach (var connection in connections.Where(c => c.node1 == from || c.node2 == from))
    //    {
    //        DistanceToConnection(currentConnection.OtherNode(from), connection, end);
    //    }

    //    return distance + min;
    //}
}
