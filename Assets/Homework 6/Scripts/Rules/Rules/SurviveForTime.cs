using System;
using UnityEngine;

public class SurviveForTime : IGameCondition
{
    private float _timeToSurvive;
    private float _elapsedTime;

    public event Action Completed;

    public SurviveForTime(float timeToSurvive)
    {
        _timeToSurvive = timeToSurvive;
        _elapsedTime = 0f;
    }

    public void Start()
    {
        _elapsedTime = 0f; // —брасываем таймер
    }

    public void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _timeToSurvive)
            Complete();
    }

    public void Stop() { }

    private void Complete()
    {
        Completed?.Invoke();
    }
}
