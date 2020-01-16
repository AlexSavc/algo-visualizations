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
            Vector2 pos2d = GetRandomDir(currentPos);
            currentPos = pos2d;
            Instantiate(nodePrefab, new Vector3(currentPos.x, currentPos.y, 0), Quaternion.identity);
            count--;
        }
    }

    public Vector2 GetRandomDir(Vector2 start)
    {
        Vector2 newVector = GetDir(GetRandomAngle());

        ProcessDir(newVector, start);

        return newVector;
    }

    public Vector2 IterateDir(Vector2 start)
    {
        return start;
    }

    public Vector2 GetDir(float angle)
    {
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector2(x, y);
    }

    public void ProcessDir(Vector2 dir, Vector2 start)
    {
        dir.x = dir.x * nodeDistance + start.x;
        dir.y = dir.y * nodeDistance + start.y;
    }

    public float GetRandomAngle()
    {
       return Random.Range(0, 360);
    }
}
