using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Data;
using Conffi.Item;
using Unity.VisualScripting;
using UnityEngine;

namespace Confii.Objects
{

    public sealed class DebugGrid : MonoBehaviour
    {
        [SerializeField] private SofaPresenter _presenter;
        [SerializeField] private Segment segment;
        [SerializeField] private GridCell cellGameObject;
        [SerializeField] private bool onStartCreate;

        [SerializeField] private Transform startPoint;
        [SerializeField] private int width;
        [SerializeField] private  int height;
        [SerializeField] private  float cellSize;
        public List<GameObject> cellGO = new();
        public List<Vector3> cellPos = new();
        
        
        private Vector3 originPosition = Vector3.zero;
        private GameObject item;

        private List<Transform> gridCells = new();

        private int[,] gridArray;
        private bool drawLine;

        private void Start()
        {
            if(onStartCreate == true)
                MakeGrid(width,height,cellSize,startPoint.position);
            
            
        }
        
        private void MakeGrid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            
            //GameObject reference = Instantiate(item);
            gridArray = new int[width, height];
            
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    /*
                    tile.name = (x, y).ToString();
                    tile.transform.position = new Vector2(x, y + 1);
                    */
                    Vector3 pos = GetWorldPosition(x, 0, y);
                    cellPos.Add(pos);
                    GridCell cellG = Instantiate(cellGameObject);
                    cellG.InitCell(this,_presenter);
                    cellG.transform.position = pos;
                    //gridCells.Add(cell.transform);
                    cellGO.Add(cellG.gameObject);
                    
                    Debug.DrawLine(GetWorldPosition(x,0,y), GetWorldPosition(x,0,y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x,0,y), GetWorldPosition(x + 1,0, y), Color.white, 100f);
                }
                
                Debug.DrawLine(GetWorldPosition(0,0, height), GetWorldPosition(width,0, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width,0, 0), GetWorldPosition(width,0, height), Color.white, 100f);
                
                //Destroy(reference.gameObject);
            }
        }
        
        private Vector3 GetWorldPosition(int x, int y, int z)
        {
            return new Vector3(x, y, z) * cellSize + originPosition;
        }

        public void ResetGrid()
        {
            foreach (var cell in gridCells)
            {
                Destroy(cell.gameObject);
            }
            
            MakeGrid(width,height,cellSize,startPoint.position);
        }

        public void SetGridColorRed(Vector2 pos)
        {
            for (int i = 0; i < cellGO.Count; i++)
            {
                
            }
        }

        public void GridBuild()
        {
            foreach (var cell in cellPos)
            {
                
            }
        }

        public void GetSurroundingWallCount(int x, int y)
        {
            
        }
    }
}