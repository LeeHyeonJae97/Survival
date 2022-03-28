using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = FILE_NAME + "Joystick", menuName = MENU_NAME + "Joystick")]
public class JoystickEventChannelSO : EventChannelSO
{
    public event UnityAction<Vector2> onBeginDrag;
    public event UnityAction<Vector2> onDrag;
    public event UnityAction onEndDrag;
    public event UnityAction<Vector2> onBoundaryDoubleClicked;

    public void OnBeginDrag(Vector2 dir)
    {
        onBeginDrag?.Invoke(dir);
    }

    public void OnDrag(Vector2 dir)
    {
        onDrag?.Invoke(dir);
    }

    public void OnEndDrag()
    {
        onEndDrag?.Invoke();
    }

    public void OnBoundaryDoubleClicked(Vector2 dir)
    {
        onBoundaryDoubleClicked?.Invoke(dir);
    }
}
