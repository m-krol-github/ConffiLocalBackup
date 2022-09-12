using System.Collections.Generic;
using Conffi.Item.Pool;
using System.Collections;
using Conffi.Item;
using Confii.Objects;
using Newtonsoft.Json;
using UnityEngine;

namespace Conffi.Data
{
    public sealed class SofaPresenter : MonoBehaviour
    {
        [Header("Segments Grid Reference")]
        [Space, SerializeField] private SegmentsGrid grid;
        
        [Space, Header("Segments pool manager reference")]
        [Space, SerializeField] private PoolManager pooling;
        
        [Space, Header("Segments types")]
        [SerializeField] private BaseSegment baseElement;
        [SerializeField] private SideSegment sideLeft;
        [SerializeField] private SideSegment sideRight;
        [SerializeField] private CornerSegment cornerLeft;
        [SerializeField] private CornerSegment cornerRight;
        
        private List<BaseSegment> pooledMid = new();
        private List<SideSegment> pooledSides = new();
        private List<MeshRenderer> pillowsMeshes = new();
        private List<Vector2> posList = new();
        
        private bool isVisible;

        public void ActivatePreset(Sofa preset)
        {
            ClearBeforeActivate();
            
            if(isVisible)
                return;
            
            GridClearCellsAndPositions();
            StartCoroutine(ShowSofaPreset(preset));
            
            isVisible = true;
        }

        
        private IEnumerator ShowSofaPreset(Sofa preset)
        {
            yield return new WaitUntil(() => isVisible == false);
            
            for (int i = 0; i < preset.Elements.Count; i++)
            {
                float posX = preset.Elements[i].x;
                float posY = preset.Elements[i].y;
                
                Vector3 pos = new Vector3(posX, 0, posY);
                BaseItem mid = null;

                switch (preset.Elements[i].ElementType)
                {
                    case ElementType.Middle:
                        
                        mid = pooling.UseObject(baseElement, pos, Quaternion.identity);
                        
                        //todo: simplify to stop using "GetComponent"
                        
                        pooledMid.Add(mid.GetComponent<BaseSegment>());
                        mid.GetComponent<BaseSegment>().GetGrid(grid);
                        break;
                    
                    case ElementType.Left:
                        
                        mid = pooling.UseObject(sideLeft, pos, Quaternion.identity);
                        pooledSides.Add(mid.GetComponent<SideSegment>());
                        break;
                    
                    case ElementType.Right:
                        mid = pooling.UseObject(sideRight, pos, Quaternion.identity);
                        pooledSides.Add(mid.GetComponent<SideSegment>());
                        break;
                    
                    case ElementType.CornerLeft:
                        //TODO: fill with required element
                        
                        break;
                    
                    case ElementType.CornerRight:
                        //TODO: fill with required element
                        
                        break;
                    
                    case ElementType.Pouf:
                        //TODO: fill with required element
                        
                        break;
                }
                
                //
                posList.Add(mid.transform.position);
                //
                CheckCells(mid);
                
                pillowsMeshes.Add(mid.Pillow.GetComponentInChildren<MeshRenderer>());

                for (int j = 0; j < pooledSides.Count; j++)
                {
                    pooledSides[j].SetSide(preset.Elements[i].ElementSideType);
                }
            }
        }
        
        public void SetSideType(ElementSideType type)
        {
            if (type == ElementSideType.Large)
            {
                foreach (var side in pooledSides)
                {
                    if(side.GetComponent<SideSegment>())
                        side.GetComponent<SideSegment>().SetSide(ElementSideType.Large);
                }
            }
            else if (type == ElementSideType.Thin)
            {
                foreach (var side in pooledSides)
                {
                    if(side.GetComponent<SideSegment>())
                        side.GetComponent<SideSegment>().SetSide(ElementSideType.Thin);
                }
            }
        }

        #region PUBLIC_FOR_TESTING

        private void CheckCells(BaseItem mid)
        {
            for (int j = 0; j < grid.cellGO.Count; j++)
            {
                if (grid.cellGO[j].transform.position == mid.transform.position)
                {
                    GridCell cell = grid.cellGO[j].GetComponent<GridCell>(); 
                        
                    cell.SetColor(Color.red);
                }
                else if (grid.cellGO[j].transform.position == mid.transform.position + Vector3.forward)
                {
                    GridCell cell = grid.cellGO[j].GetComponent<GridCell>(); 
                        
                    if(cell.IsCellOccupied == false)
                        cell.SetColor(Color.green);
                }
                else if (grid.cellGO[j].transform.position == mid.transform.position + Vector3.back)
                {
                    GridCell cell = grid.cellGO[j].GetComponent<GridCell>(); 
                        
                    if(cell.IsCellOccupied == false)
                        cell.SetColor(Color.green);
                }
                else if (grid.cellGO[j].transform.position == mid.transform.position + Vector3.left)
                {
                    GridCell cell = grid.cellGO[j].GetComponent<GridCell>(); 
                        
                    if(cell.IsCellOccupied == false)
                        cell.SetColor(Color.green);
                }
                else if (grid.cellGO[j].transform.position == mid.transform.position + Vector3.right)
                {
                    GridCell cell = grid.cellGO[j].GetComponent<GridCell>(); 
                        
                    if(cell.IsCellOccupied == false)
                        cell.SetColor(Color.green);
                }
            }
        }

        public void ClearBeforeActivate()
        {
            if(!isVisible)
                return;
            
            for (int i = 0; i < pooledSides.Count; i++)
            {
                pooling.ReturnObject(pooledSides[i].gameObject,0);
            }
            for (int i = 0; i < pooledMid.Count; i++)
            {
                pooling.ReturnObject(pooledMid[i].gameObject,0);
            }
            
            pooledSides.Clear();
            pooledMid.Clear();
            posList.Clear();
            pillowsMeshes.Clear();
            
            isVisible = false;
        }
        
        public void SetSide(bool large)
        {
            switch (large)
            {
                case true:
                {
                    foreach (var side in pooledSides)
                    {
                        if(side.GetComponent<SideSegment>())
                            side.GetComponent<SideSegment>().SetSide(ElementSideType.Large);
                    }

                    break;
                }
                case false:
                {
                    foreach (var side in pooledSides)
                    {
                        if(side.GetComponent<SideSegment>())
                            side.GetComponent<SideSegment>().SetSide(ElementSideType.Thin);
                    }

                    break;
                }
            }
        }

        public void GridClearCellsAndPositions()
        {
            grid.ResetGrid();
        }

        #endregion
    }
}