using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyArrow : MonoBehaviour
{
    public GameObject target;
    private Vector3 dir;

    private void Update()
    {
        dir = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir, transform.up);
    }
}
