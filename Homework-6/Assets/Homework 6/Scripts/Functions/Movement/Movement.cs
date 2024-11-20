using UnityEngine;

public class Movement
{
    private float _speed;
    private Rigidbody _rigidbody;

    public Movement(Rigidbody rigidbody, float speed)
    {
        _speed = speed;
        _rigidbody = rigidbody;
    }

    public void MoveTo(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _speed, ForceMode.Force);
    }
}
