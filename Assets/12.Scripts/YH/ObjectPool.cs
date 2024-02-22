using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int poolSize = 50;
    public int judgePoolSize = 20;

    public Queue<GameObject> poolQueue = new Queue<GameObject>();
    public Queue<GameObject> judgePoolQueue = new Queue<GameObject>();

    private void Awake()
    {
        Managers.Pool = this;
    }
    public void SetPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Managers.Resource.Instantiate($"NOTE{Managers.Game.currentStage}", transform);
            
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        for (int i = 0; i < judgePoolSize; i++)
        {
            GameObject judgeObj = Managers.Resource.Instantiate("Notes/JudgeNote.prefab", transform);
            judgeObj.SetActive(false);
            judgePoolQueue.Enqueue(judgeObj);
        }
    }

    public GameObject SpawnFromPool(bool isTrap)
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            poolQueue.Enqueue(obj);
            obj.GetComponent<Note>().isTrap = isTrap;
            obj.SetActive(true);
            return obj;
        }
        else return null;
    }

    public GameObject SpawnFromJudgePool()
    {
        if (judgePoolQueue.Count > 0)
        {
            GameObject obj = judgePoolQueue.Dequeue();
            judgePoolQueue.Enqueue(obj);
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
