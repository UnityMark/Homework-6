using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CameraFollower _follower;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _transform;
    [SerializeField] private GameRules _gameRules;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        Player character = Instantiate(_player, _transform.position, Quaternion.identity);
        Health health = new Health(100);
        character.Initialize(health);
        _follower.Initialize(character.transform);
        _healthBar.Initialize(health);
        _gameRules.Stop += character.Stop;
        character.Died += _gameRules.OnPlayerDied;
    }
}
