using System;
using UnityEngine;

public class Health
{
    public event Action<int> Changed;

    private int _health;
    private readonly int _maxHealth;

    public Health (int health)
    {
        _health = health;
        _maxHealth = health;
    }

    public void AddHealth(int value)
    {
        if (value < 0)
        {
            return;
        }
        
        _health += value;

        Mathf.Clamp(_health, 0, _maxHealth);

        Changed?.Invoke(_health);
    }

    public void ReduceHealth(int value)
    {
        if (value < 0)
        {
            return;
        }

        _health -= value;

        Mathf.Clamp(_health, 0, _maxHealth);

        Changed?.Invoke(_health);
    }

    public int GetHealth() => _health;
    public int GetMaxHealth() => _maxHealth;
}
