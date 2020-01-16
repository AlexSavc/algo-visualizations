using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    public List<Node> connected;
    LineRenderer lineRend;
    public string nodeName;

    void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    public void SetConnected(Node[] nodes)
    {

    }

    public void SetupLines()
    {
        foreach(Node node in connected)
        {

        }
    }
}
