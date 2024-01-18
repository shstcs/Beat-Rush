using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int skillPrecision = 100;
    public float speed = 10f;
    public float distance = 30f;


    public LayerMask targetLayerMask;

    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(SkillAttack());
        Destroy(gameObject, distance / speed);
    }

    IEnumerator SkillAttack()
    {
        Vector3 objLocalScale = new Vector3(transform.localScale.x, 1f, 1f);
        for (int i = 0; i < skillPrecision; i++)
        {
            objLocalScale.x += 9f / skillPrecision;
            transform.localScale = objLocalScale;
            yield return new WaitForSeconds(0.5f / skillPrecision);
        }
        _rb.velocity = Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(targetLayerMask.value == (targetLayerMask.value | (1 << other.gameObject.layer)))
        {
            other.GetComponent<Note>().BreakNote();
            Managers.Game.Combo++;
            Managers.Game.AddScore(50 + Managers.Game.Combo);
        }

    }
}
