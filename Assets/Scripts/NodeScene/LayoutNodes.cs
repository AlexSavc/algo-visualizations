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
    public float nodeDistance = 2;
    public float minNodeDistance = 1;

    public GameObject nodeParent;

    private Hashtable nodePositions;

    public void Awake()
    {
        nodePositions = new Hashtable();
    }

    public void Start()
    {
        SetupNodes();
    }

    public void SetupNodes()
    {
        PlaceNodes();
    }

    private void PlaceNodes()
    {
        int count = numOfNodes;
        Vector2 currentPos = Camera.main.transform.position;
        while (count > 0)
        {
            SpawnNode(currentPos, out currentPos);
            count--;
        }
    }

    public void SpawnNode(Vector2 startPos, out Vector2 newPos)
    {
        Vector2 pos2d = GetNextDir(startPos);
        
        Node neuNode = Instantiate(nodePrefab, new Vector3(pos2d.x, pos2d.y, 0), Quaternion.identity, nodeParent.transform);
        neuNode.gameObject.name = "Node " + (nodePositions.Count+1);

        nodePositions.Add(neuNode.gameObject.name, neuNode.transform.localPosition);
        newPos = pos2d;
    }

    public Vector2 GetNextDir(Vector2 start)
    {
        Vector2 neu = GetRandomDir(start);
        if (!HasRoomAt(neu))
        {
            neu = IterateDir(start);
        }

        return neu;
    }

    public Vector2 GetRandomDir(Vector2 start)
    {
        Vector2 newVector = GetDir(GetRandomAngle());

        newVector = ProcessDir(newVector, start);

        return newVector;
    }

    public Vector2 IterateDir(Vector2 start)
    {
        int increment = 120;

        while(increment > 1)
        {
            for(int i = 0; i < 360; i += increment)
            {
                Vector2 Try = ProcessDir(GetDir(increment), start);
                if (HasRoomAt(Try))
                {
                    return Try;
                }
            }
            increment /= 2;
        }

        throw new System.Exception("Couldnt find a way, bro");
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

}
