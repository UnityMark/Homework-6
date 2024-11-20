using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable, IMovable
{
    public event Action Died;

    [Header("Componets")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Settings Enemy")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    [Header("Settings Raycast")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _distance;


    private Health _health;
    private Movement _mover;
    private Rotator _rotator;
    private Cooldown _cooldown;
    private Direction _direction;
    private RaycastHandler _raycastHandler;

    private bool _isDead = false;
    private Vector3 _directionTo;
    private bool _isStop = false;

    public void Initialize(Health health)
    {
        _mover = new Movement(_rigidbody, _speed);
        _rotator = new Rotator(this.transform, _speedRotation);

        _direction = new Direction();

        _cooldown = new Cooldown(5, true);
        _cooldown.OnTimeEnd += OnDirectionChanged;
        OnDirectionChanged();

        _raycastHandler = new RaycastHandler(_layerMask, _distance, this.transform);


        _health = health;
        _health.Changed += OnHealthChanged;
    }

    private void Update()
    {
        if(_isDead || _isStop) return;
        _cooldown.UpdateTime();
    }

    private void FixedUpdate()
    {
        if (_isDead || _isStop) return;

        if(_raycastHandler.GetTarget(_directionTo) != null) OnDirectionReverse();

        Movement();
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
        _cooldown.OnTimeEnd -= OnDirectionChanged;
    }

    public void Stop() => _isStop = true;

    public void TakeDamage(int damage) => _health.ReduceHealth(damage);

    public void Movement()
    {
        _mover.MoveTo(_directionTo);
        _rotator.RotateTo(_directionTo);
    }

    private void OnHealthChanged(int health)
    {
        if (_health.GetHealth() <= 0)
        {
            _isDead = true;
            Died?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void OnDirectionChanged() => _directionTo = _direction.GetDirectionRandom();

    private void OnDirectionReverse() => _directionTo *= -1;

    private void OnCollisionStay(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null) 
        {
            player.TakeDamage(1);
        }
    }
}
