using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Data;
using Conffi.Item.Pool;
using Confii.Objects;
using Unity.Mathematics;
using UnityEngine;

namespace Conffi.Item
{
    public sealed class BaseSegment : BaseItem
    {
        [SerializeField] private SegmentsGrid grid;
        
        private Vector3 up = Vector3.forward;
        private Vector3 down = Vector3.back;
        private Vector3 left = Vector3.left;
        private Vector3 right = Vector3.right;
        
        private BaseSegment _baseSegment;
        
        protected override void Awake()
        {
            base.Awake();

            _baseSegment = this;
        }

        public override void ShowPillow(bool show)
        {
            base.ShowPillow(show);
        }

        public void GetGrid(SegmentsGrid grid)
        {
            this.grid = grid;
            CheckSides();
        }
        
        private void CheckSides()
        {
            foreach (var t in grid.cellGO)
            {
                if (t.transform.position == this.transform.position + Vector3.left)
                {
                    Debug.Log("cannot Place");
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
        }
    }
}