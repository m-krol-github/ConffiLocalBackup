using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conffi.Item.Accessories
{
    [System.Serializable]
    public sealed class Pillow : MonoBehaviour
    {
        [SerializeField] private GameObject pillowPrefab;
        [SerializeField] private Vector3 pillowPrefabPosition;
    }
}