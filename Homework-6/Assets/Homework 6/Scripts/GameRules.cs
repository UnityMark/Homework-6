using System;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public Action Stop;

    [Header("Win Conditions")]
    public WinCondition _winCondition;
    public float _survivalTime = 30f; // Время, которое нужно продержаться
    public int _enemiesToKill = 10;  // Количество врагов, которое нужно убить

    [Header("Lose Conditions")]
    public LoseCondition _loseCondition;
    public int _maxEnemies = 20; // Максимальное количество врагов, которые могут заспавниться

    private float _currentTime = 0f; // Текущее время выживания
    private int _killedEnemies = 0; // Счетчик убитых врагов
    private int _spawnedEnemies = 0; // Счетчик заспавненных врагов

    private bool _isGameOver = false;

    public bool IsGameOver => _isGameOver;

    private void Update()
    {
        if (_isGameOver) return;

        switch (_winCondition)
        {
            case WinCondition.SurviveForTime:
                _currentTime += Time.deltaTime;
                if (_currentTime >= _survivalTime)
                {
                    WinGame();
                }
                break;

            case WinCondition.KillEnemies:
                if (_killedEnemies >= _enemiesToKill)
                {
                    WinGame();
                }
                break;
        }

        if(_loseCondition == LoseCondition.TooManyEnemies)
        {
            if (_spawnedEnemies > _maxEnemies)
            {
                LoseGame();
            }
        }
    }

    public void OnEnemyKilled() => _killedEnemies++;

    public void OnEnemySpawned() => _spawnedEnemies++;

    public void OnPlayerDied()
    {
        if (_loseCondition == LoseCondition.PlayerDied)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        _isGameOver = true;
        Stop?.Invoke();
        Debug.Log("You Win!");
    }

    private void LoseGame()
    {
        _isGameOver = true;
        Stop?.Invoke();
        Debug.Log("You Lose!");
    }

}

public enum WinCondition
{
    SurviveForTime,
    KillEnemies
}

public enum LoseCondition
{
    PlayerDied,
    TooManyEnemies
}