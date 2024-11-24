using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private TimerSpawn _timer;
    [SerializeField] private GameRules _gameRules;
    [SerializeField] private float _spawnCooldown = 10f;

    private Cooldown _cooldown;
    private bool _canSpawn = true;

    public event Action OnEnemySpawned;

    private void Awake()
    {
        _cooldown = new Cooldown(_spawnCooldown, true);
        _cooldown.OnTimeEnd += Spawn;
        _timer.Initialize(_cooldown);
    }

    private void OnDestroy()
    {
        _cooldown.OnTimeEnd -= Spawn;
    }

    private void Update()
    {
        if (_canSpawn)
        {
            _cooldown.UpdateTime();
        }
    }

    private void Spawn()
    {
        if (!_canSpawn) return;
        Enemy enemy = Instantiate(_enemy, _spawnPoint.position, Quaternion.identity);
        Health health = new Health(100);
        enemy.Initialize(health);
        SetCondition(enemy);
        OnEnemySpawned?.Invoke();
    }

    private void SetCondition(Enemy enemy)
    {
        if (_gameRules.WinCondition is KillEnemies winKillEnemyCondition)
        {
            enemy.Died += winKillEnemyCondition.OnEnemyKilled;
        }
        else if (_gameRules.LoseCondition is KillEnemies loseKillEnemyCondition)
        {
            enemy.Died += loseKillEnemyCondition.OnEnemyKilled;
        }

        if (_gameRules.WinCondition is MaxEnemies winMaxEnemiesCondition)
        {
            OnEnemySpawned += winMaxEnemiesCondition.OnEnemySpawned;
        }
        else if (_gameRules.LoseCondition is MaxEnemies loseMaxEnemiesCondition)
        {
            OnEnemySpawned += loseMaxEnemiesCondition.OnEnemySpawned;
        }
    }

    private void Stop()
    {
        _canSpawn = false;
    }
}
