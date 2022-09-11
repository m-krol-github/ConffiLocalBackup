using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Item.Accessories;
using UnityEngine;

namespace Conffi.Item
{
    public abstract class BaseItem : MonoBehaviour
    {
        [SerializeField] private Pillow pillow;
        public Pillow Pillow => pillow;

        [SerializeField] protected MeshRenderer renderer;
        
        protected virtual void Awake()
        {
            renderer = GetComponent<MeshRenderer>();
        }
        
        public virtual void ShowPillow(bool show)
        {
            switch (show)
            {
                case true:
                    pillow.gameObject.SetActive(true);
                    break;
                case false:
                    pillow.gameObject.SetActive(false);
                    break;
            }
        }
    }
}