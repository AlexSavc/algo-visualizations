using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    private Hashtable built;
    public Node parent;
    public Node[] connected;
    //bool[] built;
    public string nodeName;
    public Material lineMaterial;
    //public int totalChildren = 0;
    public SpriteRenderer rend;
    public Color visitedColor = Color.blue;
    public int nodeNum;

    void Awake()
    {
        built = new Hashtable();
    }

    public void SetConnected(Node[] nodes, Node Parent)
    {
        connected = nodes;
        parent = Parent;
        SetupLines();
    }

    /*public void SetTotalChildren(int i)
    {
        totalChildren = i;
        built = new bool[i];
    }*/

    public void SetParent(Node node)
    {
        parent = node;
        AddSelfToParent();
    }

    private void AddSelfToParent()
    {
        parent.AddConnected(this);
    }

    public void AddConnected(Node node)
    {
        List<Node> list = new List<Node>();
        foreach(Node n in connected)
        {
            list.Add(n);
        }

        list.Add(node);
        connected = list.ToArray();

        SetupLines();
    }

    private void SetupLines()
    {
        try
        {
            for(int i = 0; i < connected.Length; i++)
            {
                if (built.ContainsKey(connected[i].nodeNum)) continue;
                Node node = connected[i];
                GameObject lineObj = new GameObject("line to " + node.nodeName);
                lineObj.transform.SetParent(transform);
                LineRenderer lineRend = lineObj.AddComponent<LineRenderer>();

                lineRend.positionCount = 2;
                lineRend.SetPosition(0, transform.position);
                lineRend.SetPosition(1, node.gameObject.transform.position);
                lineRend.startColor = Color.white;
                lineRend.endColor = Color.white;
                lineRend.startWidth = 0.1f;
                lineRend.endWidth = 0.1f;
                lineRend.material = lineMaterial;
                lineRend.sortingLayerName = "Lines";
                built.Add(connected[i].nodeNum, true);
            }
        }
        catch (System.Exception) { }
    }

    public void SetVisited()
    {
        rend.color = visitedColor;
        LineRenderer[] rends = parent.gameObject.GetComponentsInChildren<LineRenderer>();

        foreach(LineRenderer rend in rends)
        {
            rend.startColor = Color.gray;
            rend.endColor = Color.gray;
        }
    }
}