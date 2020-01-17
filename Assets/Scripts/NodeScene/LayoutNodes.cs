using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LayoutNodes : MonoBehaviour
{
    public Node nodePrefab;
    public Node head;
    public enum NodeMapType { tree, graph }
    public NodeMapType nodeMapType;
    public int numOfNodes = 100;
    public int numOfBranches = 3;
    public int maxGraphConnections = 4;
    public float nodeDistance = 1;
    public float minNodeDistance = 0.5f;

    public GameObject nodeParent;

    public TMP_InputField numOfNodesInput;
    public TMP_InputField numOfBranchesInput;
    public TMP_InputField nodeDistanceInput;
    public TMP_InputField minNodeDistanceInput;

    public delegate void CheckNode(Node toCheck);
    public CheckNode checkNode;

    private Hashtable nodePositions;

    private Node currentNode;
    

    public void SetupNodes()
    {
        nodePositions = new Hashtable();
        if (nodeParent != null)
        {
            Destroy(nodeParent);
        }
        nodeParent = new GameObject("NodeParent");
        SetValues();
        LayoutTree();
    }

    public void LayoutTree()
    {
        Node neuNode = SpawnNode(Vector3.zero, out Vector2 neu);
        head = neuNode;

        BreadthFirstSearch BFS;
        if (GetComponent<BreadthFirstSearch>() == null)
            BFS = (BreadthFirstSearch)gameObject.AddComponent(typeof(BreadthFirstSearch));
        else BFS = GetComponent<BreadthFirstSearch>();

        BFS.Generate(neuNode, SpawnNodes, numOfNodes * numOfBranches);
    }

    public void LayoutGraph()
    {
        
    }

    public Node SpawnNode(Vector2 startPos, out Vector2 newPos)
    {
        Vector2 pos2d = GetNextDir(startPos, out bool success);
        
        Node neuNode = Instantiate(nodePrefab, new Vector3(pos2d.x, pos2d.y, 0), Quaternion.identity, nodeParent.transform);
        neuNode.gameObject.name = "Node " + (nodePositions.Count+1);

        nodePositions.Add(neuNode.gameObject.name, neuNode.transform.localPosition);

        //!\\THIS ONLY WORKS FOR HEAD NODE
        neuNode.nodeNum = nodePositions.Count - 1;
        //neuNode.SetTotalChildren(1);
        
        currentNode = neuNode;

        newPos = pos2d;
        return neuNode;
    }

    public void SpawnNodes(Node node)
    {
        Vector2 startPos = node.transform.position;
        //node.SetTotalChildren(numOfBranches);
        for(int i = 0; i < numOfBranches; i++)
        {
            Vector2 pos2d = GetNextDir(startPos, out bool success);
            if (success)
            {
                Node neuNode = Instantiate(nodePrefab, new Vector3(pos2d.x, pos2d.y, 0), Quaternion.identity, nodeParent.transform);

                neuNode.gameObject.name = 
                neuNode.nodeName = "Node " + (nodePositions.Count + 1);
                neuNode.nodeNum = nodePositions.Count;

                nodePositions.Add(neuNode.gameObject.name, neuNode.transform.localPosition);
                //neuNode.totalChildren += 1;

                neuNode.SetParent(node);
            }

        }
    }

    public Vector2 GetNextDir(Vector2 start, out bool success)
    {
        success = true;
        Vector2 neu = GetRandomDir(start);
        if (!HasRoomAt(neu))
        {
            neu = IterateDir(start, out success);
        }
        
        return neu;
    }

    public Vector2 GetRandomDir(Vector2 start)
    {
        Vector2 newVector = GetDir(GetRandomAngle());

        newVector = ProcessDir(newVector, start);

        return newVector;
    }

    public Vector2 IterateDir(Vector2 start, out bool success)
    {
        int increment = 120;
        //int add = 0;
        while(increment > 1)
        {
            //add += 1 / 2;
            //minNodeDistance += add;
            for(int i = 0; i < 360; i += increment)
            {
                Vector2 Try = ProcessDir(GetDir(increment), start);
                if (HasRoomAt(Try))
                {
                    success = true;
                    return Try;
                }
            }
            increment /= 2;
        }

        success = false;
        return start;
    }

    public Vector2 ProcessDir(Vector2 dir, Vector2 start)
    {
        dir.x = dir.x * nodeDistance + start.x;
        dir.y = dir.y * nodeDistance + start.y;

        return new Vector2(dir.x, dir.y);
    }

    public bool HasRoomAt(Vector2 pos)
    {
        foreach(DictionaryEntry entry in nodePositions)
        {
            Vector2 p = (Vector3)entry.Value;
            if (IsTooClose(p, pos))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsTooClose(Vector2 first, Vector2 second)
    {
        if (Vector2.Distance(first, second) < minNodeDistance) { return true; }
        else return false;
    }

    public Vector2 GetDir(float angle)
    {
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector2(x, y);
    }


    public float GetRandomAngle()
    {
       return Random.Range(0, 360);
    }

    public bool IsNodeCorrect(Node node)
    {
        return false;
    }

    public void SetValues()
    {
        numOfNodes = int.Parse(numOfNodesInput.text);
        numOfBranches = int.Parse(numOfBranchesInput.text);
        float.TryParse(nodeDistanceInput.text, out nodeDistance);
        float.TryParse(minNodeDistanceInput.text, out minNodeDistance);
    }

    public void BFS()
    {
        if (head == null) { Debug.LogError("Head Node is null"); return; }

        BreadthFirstSearch BFS;
        if (GetComponent<BreadthFirstSearch>() == null)
            BFS = (BreadthFirstSearch)gameObject.AddComponent(typeof(BreadthFirstSearch));
        else BFS = GetComponent<BreadthFirstSearch>();

        StartCoroutine(BFS.Traverse(head));
    }
}
