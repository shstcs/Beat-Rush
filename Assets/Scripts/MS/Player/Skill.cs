using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Skill : MonoBehaviour
{
    private float _speed;
    private float _distance;
    [SerializeField]
    private LayerMask targetLayerMask;

    private Rigidbody _rb;
    private GameManager _gameManager;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _gameManager = Managers.Game;

        Init();

        StartCoroutine(SkillAttack());
        Destroy(gameObject, _distance / _speed);
    }

    private void Init()
    {
        _speed = Managers.Player.CurrentSkillData.GetSpeed();
        _distance = Managers.Player.CurrentSkillData.GetDistance();
    }

    IEnumerator SkillAttack()
    {
        transform.Rotate(new Vector3(0, 80f, 0));
        yield return new WaitForSeconds(0.5f);
        _rb.velocity = Vector3.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetLayerMask.value == (targetLayerMask.value | (1 << other.gameObject.layer)))
        {
            other.GetComponent<Note>().BreakNote();
            _gameManager.Combo++;
            if(_gameManager.Combo > _gameManager.MaxCombo)
                _gameManager.MaxCombo = _gameManager.Combo;
            _gameManager.AddScore(50 + _gameManager.Combo);
            _gameManager.judgeNotes[(int)Score.Great]++;
        }

    }
}
