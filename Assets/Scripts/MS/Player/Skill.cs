using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;
    [SerializeField]
    private LayerMask targetLayerMask;

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
        transform.Rotate(new Vector3(0, 80f, 0));
        yield return new WaitForSeconds(0.5f);
        _rb.velocity = Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetLayerMask.value == (targetLayerMask.value | (1 << other.gameObject.layer)))
        {
            other.GetComponent<Note>().BreakNote();
            Managers.Game.Combo++;
            if(Managers.Game.Combo > Managers.Game.MaxCombo)
                Managers.Game.MaxCombo = Managers.Game.Combo;
            Managers.Game.AddScore(50 + Managers.Game.Combo);
        }

    }
}
