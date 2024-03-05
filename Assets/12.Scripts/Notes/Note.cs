using UnityEngine;
using UnityEngine.VFX;

public class Note : MonoBehaviour
{
    private ParticleSystem _particle;
    private Collider _noteCollider;

    [HideInInspector]
    public int noteNumber = 0;
    public int stage = 0;
    public float mode;

    [HideInInspector]
    public bool IsJudgeNoteCreated;
    [HideInInspector]
    public GameObject LeftJudgeNote;
    [HideInInspector]
    public GameObject RightJudgeNote;

    private VisualEffectAsset _effect;
    private VisualEffectAsset _trapEffect;
    protected void Awake()
    {
        GameObject particlePrefab;
        _noteCollider = GetComponent<Collider>();

        particlePrefab = Managers.Resource.Load<GameObject>($"PAT{Managers.Game.currentStage}");
        _particle = particlePrefab.GetComponent<ParticleSystem>();

        if (Managers.Game.currentStage == 3)
        {
            _effect = Resources.Load<VisualEffectAsset>("Fireball");
            _trapEffect = Resources.Load<VisualEffectAsset>("Fireball_Trap");
        }
    }

    private void Start()
    {
        _particle.Stop();
        Vector3 colliderSize = _noteCollider.bounds.size;
        ResizeCollider(colliderSize);
    }

    private void ResizeCollider(Vector3 colliderSize)
    {
        BoxCollider boxCollider = _noteCollider as BoxCollider;
        CapsuleCollider capsuleCollider = _noteCollider as CapsuleCollider;

        if (capsuleCollider != null)
        {
            capsuleCollider.radius = colliderSize.z * Managers.Game.speedModifier;
        }
        if (boxCollider != null)
        {
            boxCollider.size = colliderSize * Managers.Game.speedModifier;
        }
    }

    protected void Update()
    {
        if (Time.timeScale > 0)
        {
            if (mode == 3)
            {
                if (gameObject.transform.position.z < 10)
                {
                    Managers.Game.Combo++;
                    Managers.Game.AddScore(100 + Managers.Game.Combo);
                    Managers.Game.judgeNotes[(int)Score.Perfect]++;
                    Managers.Game.curJudge = "Perfect";
                    Debug.Log(noteNumber);
                    BreakNote();
                }
            }
            else if (gameObject.transform.position.z < 8)
            {
                if (mode == 1)
                {
                    Managers.Game.Combo++;
                    Managers.Game.AddScore(50 + Managers.Game.Combo);
                    BreakNote();
                }
                else
                {
                    Managers.Game.Combo = 0;
                    BreakNote();
                    Managers.Game.curJudge = "Miss";
                    Managers.Game.judgeNotes[(int)Score.Miss]++;
                    if (Managers.Game.currentStage != 0)
                    {
                        Managers.Player.ChangeHealth(-1);
                        Managers.Game.CallDamaged();
                    }
                }
            }
            else if (gameObject.transform.position.z < 9.5 && mode == 1)
            {
                _noteCollider.enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        if (Managers.Game.mode == GameMode.normal)
        {
            ChangeColor();
        }

        gameObject.GetComponent<Collider>().enabled = true;
        if (Managers.Game.mode == GameMode.Sudden)
        {
            foreach (Transform child in transform)
            {
                // 자식 오브젝트 비활성화
                child.gameObject.SetActive(false);
            }
        }
    }

    public void ShowNote()
    {
        foreach (Transform child in transform)
        {
            // 자식 오브젝트 활성화
            child.gameObject.SetActive(true);
        }
        if (Managers.Game.currentStage == 3)
        {
            ChangeColor();
        }
    }

    public void BreakNote()
    {
        if (mode == 3)
        {
            Managers.Sound.PlayBGM((BGM)stage);
        }
        ParticleSystem _destroyParticle = Instantiate(_particle);
        _destroyParticle.transform.position = transform.position;
        _particle.Play();
        transform.position = Vector3.zero;
        Managers.Game.stageInfos[stage].curNoteInStage[noteNumber]++;

        IsJudgeNoteCreated = false;
        if (LeftJudgeNote != null)
            LeftJudgeNote.SetActive(false);
        if (LeftJudgeNote != null)
            RightJudgeNote.SetActive(false);

        gameObject.SetActive(false);
    }

    private void ChangeColor()
    {
        if (Managers.Game.currentStage == 3)
        {
            if (mode == 1)
            {
                gameObject.GetComponentInChildren<VisualEffect>().visualEffectAsset = _trapEffect;
            }
            else
            {
                gameObject.GetComponentInChildren<VisualEffect>().visualEffectAsset = _effect;
            }
        }
    }
}
