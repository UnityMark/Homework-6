using System;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public Action Stop;

    [Header("Win Conditions")]
    public WinCondition _winCondition;
    public float _survivalTime = 30f; // �����, ������� ����� ������������
    public int _enemiesToKill = 10;  // ���������� ������, ������� ����� �����

    [Header("Lose Conditions")]
    public LoseCondition _loseCondition;
    public int _maxEnemies = 20; // ������������ ���������� ������, ������� ����� ������������

    private float _currentTime = 0f; // ������� ����� ���������
    private int _killedEnemies = 0; // ������� ������ ������
    private int _spawnedEnemies = 0; // ������� ������������ ������

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