using UnityEngine;

public class LobbyArrow : MonoBehaviour
{
    public GameObject Stage1Object;
    public GameObject Stage2Object;
    public GameObject Stage3Object;
    private Vector3 dir;

    private GameObject _target;

    private void Start()
    {
        if (Managers.Data.CurrentStateData.CurrentClearStage > 3) Destroy(gameObject);
        else
        {
            _target = Managers.Data.CurrentStateData.CurrentClearStage switch
            {
                1 => Stage1Object,
                2 => Stage2Object,
                3 => Stage3Object,
            };
        }
    }

    private void Update()
    {
        dir = _target.transform.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir, transform.up);
    }
}
