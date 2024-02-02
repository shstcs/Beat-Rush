using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int poolSize = 50;

    public Queue<GameObject> poolQueue = new Queue<GameObject>();

    private void Awake()
    {
        Managers.Pool = this;
    }
    public void SetPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj;
            if (Managers.Game.currentStage == 0)
            {
                obj = Managers.Resource.Instantiate("Notes/Clock.prefab", transform);
            }
            else if (Managers.Game.currentStage == 1)
            {
                obj = Managers.Resource.Instantiate("Notes/Rock.prefab", transform);
            }
            else
            {
                obj = Managers.Resource.Instantiate("Notes/Ice.prefab", transform);
            }
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject SpawnFromPool()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            poolQueue.Enqueue(obj);
            obj.SetActive(true);
            return obj;
        }
        else return null;
    }

    public List<GameObject> GetActiveAliveNotes()
    {
        List<GameObject> activepool = new List<GameObject>();
        foreach (GameObject obj in poolQueue)
        {
            if (obj.activeSelf == true)
            {
                activepool.Add(obj);
            }
            activepool.OrderBy(x => x.transform.position.z).ToList();
        }
        return activepool;
    }

    public List<GameObject> GetActiveAliveNotes(int noteNum)
    {
        List<GameObject> activepool = new List<GameObject>();
        foreach (GameObject obj in poolQueue)
        {
            if (obj.activeSelf == true && obj.GetComponent<Note>().noteNumber == noteNum)
            {
                activepool.Add(obj);
            }
            activepool.OrderBy(x => x.transform.position.z).ToList();
        }
        return activepool;
    }

    public List<GameObject> GetActiveNotes()
    {
        List<GameObject> activepool = new List<GameObject>();
        foreach (GameObject obj in poolQueue)
        {
            if (obj.activeSelf == true)
            {
                activepool.Add(obj);
            }
        }
        return activepool;
    }
}
