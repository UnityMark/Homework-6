using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
        _health.Changed += OnHealthChanged;

        OnHealthChanged(_health.GetHealth());
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth)
    {
        float healthInPercantage = (float) currentHealth / _health.GetMaxHealth();

        _slider.value = healthInPercantage;
    }
}
