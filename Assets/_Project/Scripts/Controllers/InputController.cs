using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public Action<Vector2> onLeftMouseClick;
    public Action<bool> onRightMouseUpAndDown;
    public Action onEscapeClick;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            onLeftMouseClick?.Invoke(Mouse.current.position.ReadValue());
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            onRightMouseUpAndDown?.Invoke(true);
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            onRightMouseUpAndDown?.Invoke(false);
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            onEscapeClick?.Invoke();
        }

    }
}
