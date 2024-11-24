using System;
using UnityEngine;
public class InputHandler
{
    private readonly string _horizontal = "Horizontal";
    private readonly string _vertical = "Vertical";
    private readonly KeyCode _keySpace = KeyCode.Space;

    public event Action OnInputMovable;
    public event Action OnInputSpace;
    public event Action OnAnimationStartRunning;
    public event Action OnAnimationStopRunning;

    public void UpdateInputMovable()
    {
        if(GetInputDirection())
        {
            OnAnimationStartRunning?.Invoke();
            OnInputMovable?.Invoke();
        }
        else
        {
            OnAnimationStopRunning?.Invoke();
        }
    }

    public void UpdateInputSpace()
    {
        if (GetInput(_keySpace))
        {
            OnInputSpace?.Invoke();
        }
    }

    public bool GetInputDirection() => Mathf.Abs(GetHorizontal()) + Mathf.Abs(GetVertical()) > 0.01f;
    public bool GetInput(KeyCode keyCode) => Input.GetKeyDown(keyCode);
    public float GetHorizontal() => Input.GetAxisRaw(_horizontal);
    public float GetVertical() => Input.GetAxisRaw(_vertical);
}
