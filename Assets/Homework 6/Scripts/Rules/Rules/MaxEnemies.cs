using System;

public class MaxEnemies : IGameCondition
{
    private int _maxEnemies;
    private int _spawnedEnemies;

    public event Action Completed;

    public MaxEnemies(int maxEnemies)
    {
        _maxEnemies = maxEnemies;
        _spawnedEnemies = 0;
    }

    public void Start() 
    {
        _spawnedEnemies = 0;
    }

    public void Update()
    {
        if (_spawnedEnemies > _maxEnemies)
            Complete();
    }

    public void Stop() { }

    public void OnEnemySpawned()
    {
        _spawnedEnemies++;
    }

    private void Complete()
    {
        Completed?.Invoke();
    }
}
