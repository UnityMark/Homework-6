using UnityEngine;

public class Shooter: Weapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _speedBullet = 1f;

    public override void Attack() => Shoot();

    public void Shoot()
    {
        Bullet bullet = Instantiate(_bullet, _parent.position, Quaternion.identity);
        bullet.Initialize(this.transform.forward.normalized, _speedBullet, _damage);
    }
}
