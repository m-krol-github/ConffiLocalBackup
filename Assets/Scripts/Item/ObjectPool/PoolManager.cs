using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conffi.Item.Pool
{
    public class PoolManager : MonoBehaviour
    {
        public PoolItem[] poolItems;

        private readonly Dictionary<int, Queue<BaseItem>> poolQueue = new();
        private readonly Dictionary<int, bool> growable = new();
        private readonly Dictionary<int, Transform> parents = new();

        private void Awake()
        {
            PoolInit();
        }

        private void PoolInit()
        {
            GameObject poolGroup = new GameObject("Pool Group");

            for (int i = 0; i < poolItems.Length; i++)
            {
                GameObject uniquePool = new GameObject("Pool Group");
                uniquePool.transform.SetParent(poolGroup.transform);

                int objectID = poolItems[i].poolItem.GetInstanceID();
                poolItems[i].poolItem.gameObject.SetActive(false);
                
                poolQueue.Add(objectID, new Queue<BaseItem>());
                growable.Add(objectID, poolItems[i].growable);
                parents.Add(objectID, uniquePool.transform);

                for (int j = 0; j < poolItems[i].poolAmount; j++)
                {
                    BaseItem temp = Instantiate(poolItems[i].poolItem, uniquePool.transform);
                    poolQueue[objectID].Enqueue(temp);
                }
            }
        }

        public BaseItem UseObject(BaseItem obj, Vector3 pos, Quaternion rot)
        {
            int objID = obj.GetInstanceID();
            
            BaseItem temp = poolQueue[objID].Dequeue();

            if (temp.gameObject.activeInHierarchy)
            {
                if (growable[objID])
                {
                    poolQueue[objID].Enqueue(temp);
                    temp = Instantiate(obj, parents[objID]);
                    temp.transform.position = pos;
                    temp.transform.rotation = rot;
                    temp.gameObject.SetActive(true);
                }
                else
                {
                    temp = null;
                }
            }
            else
            {
                temp.transform.position = pos;
                temp.transform.rotation = rot;
                temp.gameObject.SetActive(true);
            }
            
            poolQueue[objID].Enqueue(temp);
            return temp;
        }

        public void ReturnObject(GameObject obj, float delay)
        {
            if (delay == 0f)
            {
                obj.SetActive(false);
            }
            else
            {
                StartCoroutine(DelayReturn(obj, delay));
            }
        }
        
        private IEnumerator DelayReturn(GameObject obj, float delay)
        {
            while (delay > 0f)
            {
                delay -= Time.deltaTime;
                yield return null;
            }
            
            obj.SetActive(false);
        }
    }
}














