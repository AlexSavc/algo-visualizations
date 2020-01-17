﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : MonoBehaviour
{
    public void Generate(Node start, LayoutNodes.CheckNode checkNode, int totalNodes)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(start);

        bool[] visited = new bool[totalNodes];
        visited[start.nodeNum] = true; 

        while(queue.Count > 0)
        {
            Node node = queue.Dequeue();
            checkNode(node);
            Node[] neighbours = node.connected;

            foreach (Node next in neighbours)
            {
                if (visited[next.nodeNum] == false)
                {
                    visited[next.nodeNum] = true;
                    queue.Enqueue(next);
                }
            }
        }
    }

    public Node[] GetLevel(Node start, int levelBelow)
    {
        List<Node> nodes = new List<Node>();



        return nodes.ToArray();
    }
}