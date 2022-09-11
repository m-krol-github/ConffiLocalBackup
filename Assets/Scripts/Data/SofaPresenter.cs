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
        [SerializeField] private DebugGrid grid;
        [Space]
        [SerializeField] private PoolManager pooling;
        [Space]
        [SerializeField] private Segment middle;
        [SerializeField] private CornerSegment cornerL;
        [SerializeField] private CornerSegment cornerR;
        
        private List<Segment> pooledMid = new();
        private List<CornerSegment> pooledSides = new();
        private List<MeshRenderer> pillowsMeshes = new();
        private List<Vector2> posList = new();
        
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
            
            for (int i = 0; i < pooledMid.Count; i++)
            {
                pooling.ReturnObject(pooledMid[i].gameObject,0);
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