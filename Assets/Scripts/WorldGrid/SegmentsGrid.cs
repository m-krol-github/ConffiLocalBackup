using System.Collections.Generic;
using Conffi.Data;
using Conffi.Item;
using UnityEngine;

namespace Confii.Objects
{

    public sealed class SegmentsGrid : MonoBehaviour
    {
        public List<GridCell> cellGO { get; private set; } = new();
        private List<Vector3> cellPos { get; set; }= new();

        [SerializeField] private bool onStartCreate;
        [Space]
        [SerializeField] private SofaPresenter _presenter;
        [SerializeField] private GridCell cellGameObject;
        [Space]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform cellsParent;
        [Space, Header("Grid Properties")]
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        
        private Vector3 originPosition = Vector3.zero;
        private GameObject item;
        
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
                    Vector3 pos = GetWorldPosition(x, 0, y);
                    cellPos.Add(pos);
                    GridCell cellG = Instantiate(cellGameObject, pos, transform.rotation, cellsParent);
                    cellG.InitCell(this,_presenter);
                    cellGO.Add(cellG);
                }
            }
        }
        
        private Vector3 GetWorldPosition(int x, int y, int z)
        {
            return new Vector3(x, y, z) * cellSize + originPosition;
        }

        public void ResetGrid()
        {
            foreach (GridCell go in cellGO)
            {
                go.ResetCellProperty();
            }
        }
    }
}