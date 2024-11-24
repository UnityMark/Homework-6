using System;

public class DiePlayer : IGameCondition
{
    private bool _isDead = false;

    public event Action Completed;

    public void Start() { }

    public void Update()
    {
        if (_isDead)
            Complete();
    }

    public void Stop() { }

    public void OnPlayerDied()
    {
        _isDead = true;
    }

    private void Complete()
    {
        Completed?.Invoke();
    }
}
