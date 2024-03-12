using UnityEngine;

public class RotateNote : Note
{
    private float rotationSpeed = 1f;
    protected new void Update()
    {
        base.Update();

        float randomX = Random.Range(0, 360);
        float randomY = Random.Range(0, 360);
        float randomZ = Random.Range(0, 360);

        // 무작위 회전 적용
        transform.Rotate(new Vector3(randomX, randomY, randomZ) * rotationSpeed * Time.deltaTime);
    }
}
