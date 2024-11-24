using UnityEngine;

public class Rotator
{
    private float _speed;
    private Transform _transform;

    public Rotator(Transform transform, float speed)
    {
        _speed = speed;
        _transform = transform;
    }

    public void RotateTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float step = _speed * Time.deltaTime;
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }
}
