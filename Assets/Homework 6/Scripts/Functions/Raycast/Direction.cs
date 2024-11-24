using UnityEngine;

public class Direction
{
    private float _minRange = -1f;
    private float _maxRange = 2f;

    public Vector3 GetDirection(float horizontal, float verticale)
    {
        return new Vector3(horizontal, 0, verticale).normalized;
    }

    public Vector3 GetDirectionRandom()
    {
        float x = Random.Range(_minRange, _maxRange);
        float z = Random.Range(_minRange, _maxRange);
        Vector3 direction = new Vector3(x, 0, z);

        return direction.normalized;
    }
}
