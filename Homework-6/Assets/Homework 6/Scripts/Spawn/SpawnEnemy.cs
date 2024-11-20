using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _transform;
    [SerializeField] private TimerSpawn _timer;
    [SerializeField] private GameRules _gameRules;

    private Health _health;
    private Cooldown _cooldown;

    private bool _canSpawn = true;

    public void Awake()
    {
        _cooldown = new Cooldown(10, true);
        _cooldown.OnTimeEnd += Spawn;
        _timer.Initialize(_cooldown);
        _gameRules.Stop += Stop;
    }

    private void OnDestroy()
    {
        _cooldown.OnTimeEnd -= Spawn;
    }

    private void Update()
    {
        if(_canSpawn)
            _cooldown.UpdateTime();
    }

    private void Spawn()
    {
        Enemy character = Instantiate(_enemy, _transform.position, Quaternion.identity);
        _health = new Health(100);
        character.Initialize(_health);
        character.Died += _gameRules.OnEnemyKilled;
        _gameRules.Stop += character.Stop;
        _gameRules.OnEnemySpawned();
    }

    private void Stop() => _canSpawn = false;
}
