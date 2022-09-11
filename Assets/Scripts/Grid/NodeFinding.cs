using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Grid;
using UnityEngine;

public class NodeFinding : MonoBehaviour
{
    private List<Node> openList = new();
    private List<Node> closedList = new();

    private int D = 10;

    private Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void FindNode()
    {
        Node startNode = grid.NodeRequest(grid.start.transform.position);
        Node endNode = grid.NodeRequest(grid.goal.transform.position);
    }
}
