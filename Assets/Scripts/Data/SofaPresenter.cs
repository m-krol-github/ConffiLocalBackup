using System.Collections.Generic;
using Conffi.Item.Pool;
using Conffi.UI;
using Conffi.Item;
using Confii.Objects;
using Newtonsoft.Json;
using UnityEngine;

namespace Conffi.Data
{
    public sealed class SofaPresenter : MonoBehaviour
    {
        [Space]
        [SerializeField] private PoolManager pooling;
        [Space]
        [SerializeField] private Segment middle;
        [SerializeField] private CornerSegment cornerL;
        [SerializeField] private CornerSegment cornerR;
        
        [SerializeField] private List<Segment> pooledMid = new();
        [SerializeField] private List<CornerSegment> pooledSides = new();
        [SerializeField] private List<MeshRenderer> pillowsMeshes = new();
        [SerializeField] private List<Vector2> posList = new();
        [SerializeField] private DebugGrid grid;
        
        private bool isVisible;

        public void ActivatePreset(Sofa preset)
        {
            if(isVisible)
                return;
            
            for (int i = 0; i < preset.Elements.Count; i++)
            {
                float posX = preset.Elements[i].x;
                float posY = preset.Elements[i].y;
                
                Vector3 pos = new Vector3(posX, 0, posY);
                BaseItem mid = null;

                if (preset.Elements[i].ElementType == ElementType.Middle)
                {
                    mid = pooling.UseObject(middle, pos, Quaternion.identity);
                    pooledMid.Add(mid.GetComponent<Segment>());
                    mid.GetComponent<Segment>().GetGrid(grid);
                }
                else if (preset.Elements[i].ElementType == ElementType.Left)
                {
                    mid = pooling.UseObject(cornerL, pos, Quaternion.identity);
                    pooledSides.Add(mid.GetComponent<CornerSegment>());
                }
                else if (preset.Elements[i].ElementType == ElementType.Right)
                {
                    mid = pooling.UseObject(cornerR, pos, Quaternion.identity);
                    pooledSides.Add(mid.GetComponent<CornerSegment>());
                }
                
                //
                posList.Add(mid.transform.position);
                //
                
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
                }


                for (int j = 0; j < pooledSides.Count; j++)
                {
                    pooledSides[j].SetSide(preset.Elements[i].ElementSideType);
                }
                
                pillowsMeshes.Add(mid.Pillow.GetComponent<MeshRenderer>());
            }
            
            isVisible = true;
        }

        
        public void Clear()
        {
            if(!isVisible)
                return;

            for (int i = 0; i < pooledSides.Count; i++)
            {
                pooling.ReturnObject(pooledSides[i].gameObject,0);
            }
            
            
            pooledSides.Clear();
            pooledMid.Clear();
            posList.Clear();
            
            isVisible = false;
        }

        
        public void SetSideType(ElementSideType type)
        {
            if (type == ElementSideType.Large)
            {
                foreach (var side in pooledSides)
                {
                    if(side.GetComponent<CornerSegment>())
                        side.GetComponent<CornerSegment>().SetSide(ElementSideType.Large);
                }
            }
            else if (type == ElementSideType.Thin)
            {
                foreach (var side in pooledSides)
                {
                    if(side.GetComponent<CornerSegment>())
                        side.GetComponent<CornerSegment>().SetSide(ElementSideType.Thin);
                }
            }
        }
        
        public void SetSide(bool large)
        {
            switch (large)
            {
                case true:
                {
                    foreach (var side in pooledSides)
                    {
                        if(side.GetComponent<CornerSegment>())
                            side.GetComponent<CornerSegment>().SetSide(ElementSideType.Large);
                    }

                    break;
                }
                case false:
                {
                    foreach (var side in pooledSides)
                    {
                        if(side.GetComponent<CornerSegment>())
                            side.GetComponent<CornerSegment>().SetSide(ElementSideType.Thin);
                    }

                    break;
                }
            }
        }
    }
}