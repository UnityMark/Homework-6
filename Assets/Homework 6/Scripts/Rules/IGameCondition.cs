using System;

public interface IGameCondition
{
    public event Action Completed;
    public void Start();
    public void Update();
    public void Stop();
}
