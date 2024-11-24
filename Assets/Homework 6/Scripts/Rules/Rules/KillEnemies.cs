using System;

public class KillEnemies : IGameCondition
{
    private int _requiredKills;
    private int _currentKills;

    public event Action Completed;

    public KillEnemies(int requiredKills)
    {
        _requiredKills = requiredKills;
        _currentKills = 0;
    }
    
    public int RequiredKills => _requiredKills;

    public void Start()
    {
        _currentKills = 0; // —брасываем счЄтчик
    }

    public void Update() { }

    public void Stop() { }

    public void OnEnemyKilled()
    {
        _currentKills++;
        if (_currentKills >= _requiredKills)
            Complete();
    }

    private void Complete()
    {
        Completed?.Invoke();
    }
}
