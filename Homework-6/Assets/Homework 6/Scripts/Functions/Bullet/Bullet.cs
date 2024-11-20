using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private float _speed;
    private int _damage;

    public void Initialize(Vector3 direction, float speed, int damage)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Wall wall = other.GetComponent<Wall>();

        if(enemy != null)
        {
            enemy.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
        else if (wall != null)
        {
            Destroy(this.gameObject);
        }
    }
}
