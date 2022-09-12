using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Data;
using Confii.Objects;
using UnityEngine;

namespace Conffi.Item
{
    public class GridCell : BaseItem
    {
        [field: SerializeField] public bool IsCellOccupied { get; private set; }

        [SerializeField] private SegmentsGrid grid;
        [SerializeField] private SofaPresenter _presenter;
        [SerializeField] private GameObject callCube;
        [SerializeField] private Material greenColor;
        [SerializeField] private Material redColor;
        [SerializeField] private Material transparentColor;

        protected override void Awake()
        {
            base.Awake();
            SetAvailbe();
            SetColor(Color.clear);
        }

        public void InitCell(SegmentsGrid grid, SofaPresenter presenter)
        {
            this.grid = grid;
            this._presenter = presenter;
        }
        
        public void SetColor(Color color)
        {
            if (color == Color.red)
            {
                renderer.material = redColor;
                IsCellOccupied = true;
            }
            else if (color == Color.green)
            {
                renderer.material = greenColor;
            }
            else if (color == Color.clear)
            {
                renderer.material = transparentColor;
            }
        }

        public void SetAvailbe()
        {
            if (IsCellOccupied)
            {
                renderer.material = redColor;
            }
            else
            {
                renderer.material = greenColor;
            }
        }

        public void ResetCellProperty()
        {
            IsCellOccupied = false;
            renderer.material = transparentColor;
        }

        private void CheckNeighbours()
        {
            /*
            for (int j = 0; j < grid.cellGO.Count; j++)
            {
                if (grid.cellGO[j].transform.position == mid.transform.position)
                {
                    GridCell cell = grid.cellGO[j].GetComponent<GridCell>(); 
                        
                    cell.SetColor(Color.red);
                    unavailbePosList.Add(mid.transform.position);
                }
            }
            foreach (var t in grid.cellGO)
            {

                GridCell cellLeft;
                var cellL = this.transform.position + Vector3.left;
                
                for (int i = 0; i < grid.cellGO.Count; i++)
                {
                    var cell = grid.cellGO[i].transform.position + cellL;

                }
                
                it.transform.position == this.transform.position + Vector3.left; 
                
                if (t.transform.position == this.transform.position + Vector3.left)
                {
                    if (cell.IsCellOccupied)
                    {
                        renderer.material = redColor;
                    }    
                }
                
                else if (t.transform.position == this.transform.position + Vector3.back)
                {
                    Debug.Log("CAN Place"); 
                }
                
                else if (t.transform.position == this.transform.position + Vector3.right)
                {
                    Debug.Log("CAN NOT Place"); 
                }
                
                else if (t.transform.position == this.transform.position + Vector3.forward)
                {
                    Debug.Log("CAN Place");
                }
            }
            */
        }
    }
}