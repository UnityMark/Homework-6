using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameRules _gameRules;
    [SerializeField] private List<SpawnEnemy> _spawnEnemies;
    [SerializeField] private SpawnPlayer _spawnPlayer;
    [SerializeField] private Condition _winCondition;
    [SerializeField] private Condition _loseCondition;

    [SerializeField] private int _needKillEnemy = 1;
    [SerializeField] private int _needMaxEnemy = 1;
    [SerializeField] private float _needSurviveTime = 1f;

    private IGameCondition _winGameCondition;
    private IGameCondition _loseGameCondition;

    private void Awake()
    {
        _winGameCondition = SetCondition(_winCondition, _winGameCondition);
        _loseGameCondition = SetCondition(_loseCondition, _loseGameCondition);

        _gameRules.Initialize(_winGameCondition, _loseGameCondition);
    }

    private IGameCondition SetCondition(Condition condition, IGameCondition gameCondition)
    {
        if (condition == Condition.SurviveForTime)
        {
            return new SurviveForTime(_needSurviveTime);
        }
        else if (condition == Condition.KillEnemies)
        {
            return new KillEnemies(_needKillEnemy);
        }
        else if (condition == Condition.MaxEnemies)
        {
            return new MaxEnemies(_needMaxEnemy);
        }
        else
        {
            return new DiePlayer();
        }
    }
}

public enum Condition
{
    SurviveForTime,
    KillEnemies,
    PlayerDied,
    MaxEnemies,
}