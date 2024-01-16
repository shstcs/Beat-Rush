using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;
    public GameObject Player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
        {
            CheckNotes();
        }
    }

    private void CheckNotes()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(10, 5, 0.01f) / 2, Quaternion.identity, noteLayer);
        if (colliders.Length > 0)
        {
            Player.GetComponent<Player>().Rotate(colliders[0].transform);
            float distance = Mathf.Abs(transform.position.z - colliders[0].transform.position.z);
            colliders[0].GetComponent<Note>().BreakNote();
            Managers.Game.Combo++;
            Managers.Game.AddScore((distance < 0.4 ? 50 : 30) + Managers.Game.Combo);     //판정에 따라 점수 다름
            if(Managers.Instance.currentskill < 100.0f)
                Managers.Instance.currentskill += 10;
            Debug.Log(Managers.Game.Combo + " " + Managers.Game.Score);
        }
    }
}
