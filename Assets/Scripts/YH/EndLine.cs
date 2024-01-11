using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Line"))
        {
            Destroy(other.gameObject);
            Debug.Log("Ãæµ¹");
        }
    }
}
