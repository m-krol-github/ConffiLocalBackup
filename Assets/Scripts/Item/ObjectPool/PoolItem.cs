using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conffi.Item.Pool
{
    [System.Serializable]
    public class PoolItem
    {
        public BaseItem poolItem;
        public string itemName;
        public int poolAmount;
        public bool growable;
    }
}