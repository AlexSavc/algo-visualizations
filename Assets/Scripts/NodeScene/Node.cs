﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    public Node parent;
    public Node[] connected;
    public string nodeName;
    public Material lineMaterial;
    public int totalChildren = 0;
    public SpriteRenderer rend;
    public Color visitedColor = Color.blue;
    public int nodeNum;

    public void SetConnected(Node[] nodes, Node Parent)
    {
        connected = nodes;
        parent = Parent;
        SetupLines();
    }

    public void SetTotalChildren(int i)
    {
        totalChildren = i;
    }

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

        if(connected.Length == totalChildren)
        {
            SetupLines();
        }
    }

    private void SetupLines()
    {
        foreach(Node node in connected)
        {
            GameObject lineObj = new GameObject("line to " + node.nodeName);
            lineObj.transform.SetParent(transform);
            LineRenderer lineRend = lineObj.AddComponent<LineRenderer>();

            lineRend.positionCount = 2;
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, node.gameObject.transform.position);
            lineRend.startColor = Color.gray;
            lineRend.endColor = Color.gray;
            lineRend.startWidth = 0.1f;
            lineRend.endWidth = 0.1f;
            lineRend.material = lineMaterial;
            lineRend.sortingLayerName = "Lines";
        }
    }

    public void SetVisited()
    {
        rend.color = visitedColor;
    }
}
