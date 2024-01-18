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
            float distance = Mathf.Abs(transform.position.z - colliders[0].transform.position.z);
            colliders[0].GetComponent<Note>().BreakNote();
            Managers.Game.Combo++;
            Managers.Game.AddScore((distance < 0.4 ? 50 : 30) + Managers.Game.Combo);     //������ ���� ���� �ٸ�
            if(Managers.Player.CurrentStateData.SkillGauge < 100)
                Managers.Player.CurrentStateData.SkillGauge += 10;
            if(Managers.Player.CurrentStateData.SkillGauge >= 100)
            {
                Managers.Player.CurrentStateData.SkillGauge = 0;
                Managers.Player.Skill();
            }
            Debug.Log(Managers.Game.Combo + " " + Managers.Game.Score);
        }
    }
}
