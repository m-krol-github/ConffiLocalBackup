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
    public sealed class Segment : BaseItem
    {
        [SerializeField] private Material greenAdd;
        [SerializeField] private Material redAdd;
        
        [SerializeField] private DebugGrid grid;
        [SerializeField] private SofaPresenter polling;
        
        private Vector3 up = Vector3.forward;
        private Vector3 down = Vector3.back;
        private Vector3 left = Vector3.left;
        private Vector3 right = Vector3.right;
        
        private Segment segment;
        
        protected override void Awake()
        {
            base.Awake();

            segment = this;
            
        }

        public override void ShowPillow(bool show)
        {
            base.ShowPillow(show);
        }

        public void GetGrid(DebugGrid grid)
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