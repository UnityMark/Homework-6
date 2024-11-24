using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IMovable, IAttackable
{
    public event Action Died;

    [Header("Componets")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private View _view;

    [Header("Settings Player")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    private Health _health;
    private Movement _mover;
    private Rotator _rotator;
    private InputHandler _inputHandler;
    private Direction _direction;

    private bool _isDead = false;
    private bool _isStop = false;


    public void Initialize(Health health)
    {
        _mover = new Movement(_rigidbody, _speed);
        _rotator = new Rotator(this.transform, _speedRotation);

        _direction = new Direction();

        _inputHandler = new InputHandler();
        _inputHandler.OnInputMovable += Movement;
        _inputHandler.OnInputSpace += Attack;
        _inputHandler.OnAnimationStartRunning += _view.StartRunning;
        _inputHandler.OnAnimationStopRunning += _view.StopRunning;

        _health = health;
        _health.Changed += OnHealthChanged;
    }

    private void Update()
    {
        if (_isDead || _isStop) return;
        _inputHandler.UpdateInputSpace();
    }

    private void FixedUpdate()
    {
        if (_isDead || _isStop) return;
        _inputHandler.UpdateInputMovable();
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
        _inputHandler.OnInputMovable -= Movement;
        _inputHandler.OnInputSpace -= Attack;
    }

    public void Stop() => _isStop = true;

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        _health.ReduceHealth(damage);
        _view.TakeHit();
    }

    public void Attack() => _view.Shoot(_shooter.Shoot);

    public void Movement()
    {
        float horizontal = _inputHandler.GetHorizontal();
        float vertical = _inputHandler.GetVertical();

        Vector3 direction = _direction.GetDirection(horizontal, vertical);

        _mover.MoveTo(direction);
        _rotator.RotateTo(direction);
    }

    private void OnHealthChanged(int health)
    {
        if (_health.GetHealth() <= 0)
        {
            _isDead = true;
            _view.Die(_isDead);
            Died?.Invoke();
        }
    }
}
