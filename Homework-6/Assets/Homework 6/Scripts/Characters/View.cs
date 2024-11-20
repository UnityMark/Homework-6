using System;
using UnityEngine;

public class View : MonoBehaviour
{
    public Action OnMove;
    
    [SerializeField] private Animator _animator;

    private readonly int _hit = Animator.StringToHash("Hit");
    private readonly int _die = Animator.StringToHash("Die");
    private readonly int _shoot = Animator.StringToHash("Shoot");
    private readonly int _isRunning = Animator.StringToHash("Move");

    private Action _attackAnimationCallBack;

    public void TakeHit() => _animator.SetTrigger(_hit);
    public void Shoot(Action callback)
    {
        _attackAnimationCallBack = callback;
        _animator.SetTrigger(_shoot);
    }

    public void OnAttackAnimation()
    {
        _attackAnimationCallBack?.Invoke();
        _attackAnimationCallBack = null;
    }

    public void StartRunning() => _animator.SetBool("Move", true);
    public void StopRunning() => _animator.SetBool("Move", false);

    public void Die(bool value) => _animator.SetBool(_die, value);
}
