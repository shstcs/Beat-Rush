using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    void Update()
    {
        if (GetComponentsInChildren<Note>().Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
