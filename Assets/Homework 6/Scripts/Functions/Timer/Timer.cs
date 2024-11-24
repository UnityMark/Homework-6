using System;
using UnityEngine;

public class Cooldown
{
    public event Action OnTimeEnd;

    private float _time = 0;
    private float _timeTick = 0;
    private bool _isLoop = false;

    public Cooldown(float time, bool isLoop)
    {
        _time = time;
        _timeTick = time;
        _isLoop = isLoop;
    }

    public void UpdateTime()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            return;
        }
        
        if(_isLoop) RefreshTime();
    }

    private void RefreshTime()
    {
        _time = _timeTick;
        OnTimeEnd?.Invoke();
    }

    public float GetTime() => _time;
}
