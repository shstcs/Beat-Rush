using UnityEngine;

public class StageText : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (Managers.Player != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(new Vector3(0, 180f, 0));
        }
    }
}
