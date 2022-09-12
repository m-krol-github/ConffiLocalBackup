using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conffi.Item.Accessories
{
    [System.Serializable]
    public sealed class Pillow : MonoBehaviour
    {
        [field: SerializeField] public MeshRenderer pillowRenderer { get; private set; }
        
        [SerializeField] private Vector3 pillowPrefabPosition;
    }
}