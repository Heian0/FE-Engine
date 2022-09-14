using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> neighbours;
    public int NodeX;
    public int NodeZ;
    public int NodeY;

    public Node()
    {
        neighbours = new List<Node>();
    }

    public float DistanceTo(Node n)
    {
        if (n == null)
        {
            Debug.Log("error");
        }

        return Vector3.Distance(new Vector3(NodeX, 0, NodeZ), new Vector3(n.NodeX, 0, n.NodeZ));
    }
}
