using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CameraFollower _follower;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _transform;
    [SerializeField] private GameRules _gameRules;
    private Player _currentPlayer;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        _currentPlayer = Instantiate(_player, _transform.position, Quaternion.identity);
        SetCondition(_currentPlayer);
        Health health = new Health(100);
        _currentPlayer.Initialize(health);
        _follower.Initialize(_currentPlayer.transform);
        _healthBar.Initialize(health);
    }

    private void SetCondition(Player player)
    {
        if (_gameRules.WinCondition is DiePlayer winCondition)
        {
            player.Died += winCondition.OnPlayerDied;
        }
        else if (_gameRules.LoseCondition is DiePlayer loseCondition)
        {
            player.Died += loseCondition.OnPlayerDied;
        }
    }
}
