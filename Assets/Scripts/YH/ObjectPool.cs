using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public GameObject prefab;
        public int poolSize;
    }

    public List<Pool> pools;
    public Queue<GameObject> poolQueue;

    private void Awake()
    {
        poolQueue = new Queue<GameObject>();
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.poolSize; i++)
                         {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }
        }
    }

    public GameObject SpawnFromPool()
    {
        GameObject obj = poolQueue.Dequeue();
        poolQueue.Enqueue(obj);
        return obj;
    }
    
    public void ReturnToPool(GameObject obj)
    {
        poolQueue.Enqueue(obj);
    }

}
