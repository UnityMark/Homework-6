using System;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public event Action GameWon;
    public event Action GameLost;

    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;

    private bool _isGameOver = false;
    
    public IGameCondition WinCondition => _winCondition;
    public IGameCondition LoseCondition => _loseCondition;

    public void Initialize(IGameCondition winCondition, IGameCondition loseCondition)
    {
        _winCondition = winCondition;
        _loseCondition = loseCondition;

        _winCondition.Completed += WinGame;
        _loseCondition.Completed += LoseGame;

        _winCondition.Start();
        _loseCondition.Start();
    }
    
    private void OnDestroy()
    {
        _winCondition.Completed -= WinGame;
        _loseCondition.Completed -= LoseGame;
    }

    private void Update()
    {
        if (_isGameOver) return;

        _winCondition.Update();
        _loseCondition.Update();
    }

    private void WinGame()
    {
        if (_isGameOver) return;
        _isGameOver = true;
        Debug.Log("You Win!");
        GameWon?.Invoke();
    }

    private void LoseGame()
    {
        if (_isGameOver) return;
        _isGameOver = true;
        Debug.Log("You Lose!");
        GameLost?.Invoke();
    }
}
