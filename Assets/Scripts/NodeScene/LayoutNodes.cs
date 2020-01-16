using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutNodes : MonoBehaviour
{
    public Node nodePrefab;
    public Node head;
    public enum NodeMapType { tree, graph }
    public NodeMapType nodeMapType;
    public int numOfNodes = 100;
    public int numOfBranches = 3;
    public int maxGraphConnections = 4;

    public void Start()
    {

    }

    public void SetupNodes()
    {

    }

    private void PlaceNode()
    {

    }
}
