using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
        {
            if (Managers.Player.IsDie()) return;

            CheckNotes();
        }
    }

    private void CheckNotes()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(10, 5, 0.01f) / 2, Quaternion.identity, noteLayer);
        if (colliders.Length > 0)
        {

            Managers.Player.Rotate(colliders[0].transform);     

            colliders[0].GetComponent<Note>().BreakNote();      

            float distance = Mathf.Abs(transform.position.z - colliders[0].transform.position.z);

            int score;
            if (distance > 0.7f) score = 10;            //Bad
            else if (distance > 0.5) score = 30;        //Good
            else if (distance > 0.1) score = 50;        //Great
            else score = 100;                           //Perfect

            Managers.Game.Combo++;
            Managers.Game.AddScore(score + Managers.Game.Combo);     

            // Skill
            if(Managers.Player.CurrentStateData.SkillGauge < 100)
                Managers.Player.CurrentStateData.SkillGauge += 10;

            Debug.Log(Managers.Game.Combo + " " + Managers.Game.Score);
        }
    }
}
