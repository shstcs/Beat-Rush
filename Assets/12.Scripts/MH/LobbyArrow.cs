using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyArrow : MonoBehaviour
{
    public GameObject Stage1Object;
    public GameObject Stage2Object;
    public GameObject Stage3Object;
    private Vector3 dir;

    private void Update()
    {
        if (Managers.Game.currentStage > 3) Destroy(this);

        GameObject target = Managers.Game.currentStage switch
        {
            1 => Stage1Object,
            2 => Stage2Object,
            3 => Stage3Object,
        };
        dir = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir, transform.up);
    }
}
