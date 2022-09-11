using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Grid;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<Node> path;

    public LayerMask occupied;
    public GameObject start;
    public GameObject goal;
    public GameObject bottomLeft;
    public GameObject topRight;

    [SerializeField] private bool canMakeOnStart;
    
    private Node[,] myGrid;

    private int startX, startZ;
    private int endX, endZ;

    private int vCells, hCells;

    private int cellWidht = 1;
    private int cellHeight = 1;

    private void Awake()
    {
        if(canMakeOnStart)
            GridCreate();
    }

    private void GridCreate()
    {
        startX = (int)bottomLeft.transform.position.x;
        startZ = (int)bottomLeft.transform.position.z;

        endX = (int)topRight.transform.position.x;
        endZ = (int)topRight.transform.position.z;

        vCells = (int)((endX - startX)) / (cellHeight);
        hCells = (int)((endX - startX)) / (cellWidht);

        myGrid = new Node[hCells + 1, vCells + 1];

        UpdateGrid();
    }

    private void UpdateGrid()
    {
        for (int i = 0; i <= hCells; i++)
        {
            for (int j = 0; j <= vCells; j++)
            {
                bool walkable = !(Physics.CheckSphere(new Vector3(startX + 1, 0, startZ + j), .4f, occupied));

                myGrid[i, j] = new Node(i, j, 0, walkable);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (myGrid != null)
        {
            foreach (Node node in myGrid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                
                Gizmos.DrawWireCube(new Vector3(startX + node.posX, 0 , startZ + node.posY), new Vector3(.6f,.8f,.8f));
            }
        }
    }

    public Node NodeRequest(Vector3 pos)
    {
        int gridX = (int) Vector3.Distance(new Vector3(pos.x , 0,0), new Vector3(startX, 0, 0));
        int gridZ = (int) Vector3.Distance(new Vector3(0, 0,pos.z), new Vector3(startX, 0, startZ));

        return myGrid[gridX, gridZ];
    }

    public List<Node> GetNeighbourNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0)
                {
                    continue;
                }

                if (x == -1 && z == 0)
                {
                    continue;
                }

                if (x == 1 && z == 1)
                {
                    continue;
                }
                
                if (x == 1 && z == -1)
                {
                    continue;
                }
                
                if (x == -1 && z == -1)
                {
                    continue;
                }

                int checkPosX = node.posX + x;
                int checkPosZ = node.posY + z;

                if (checkPosX >= 0 && checkPosX <= (hCells + 1) && checkPosZ >= 0 && checkPosZ < (vCells + 1))
                {
                    neighbours.Add(myGrid[checkPosX, checkPosZ]);
                }
            }
        }

        return neighbours;
    }
}
