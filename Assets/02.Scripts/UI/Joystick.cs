using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 Direction { get { return _stick.anchoredPosition; } }
    private float Radius { get { return _base.sizeDelta.x / 2; } }
    private JoystickEventChannelSO JoystickEventChannel
    {
        get
        {
            if (_joystickEventChannel == null) _joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
            return _joystickEventChannel;
        }
    }

    [SerializeField] [Range(0, 1f)] private float _deadZone;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _base;
    [SerializeField] private RectTransform _stick;
    private bool _pressed;
    private JoystickEventChannelSO _joystickEventChannel;

    private void Update()
    {
        if (_pressed) JoystickEventChannel.OnDrag(Direction);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        JoystickEventChannel.OnBeginDrag(Direction);

        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        JoystickEventChannel.OnEndDrag();

        // reset stick's position
        _stick.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = eventData.position - (Vector2)(_canvas.renderMode == RenderMode.ScreenSpaceCamera ? 
            MainCamera.Camera.WorldToScreenPoint(_base.position) : _base.position);

        // set stick's position
        _stick.anchoredPosition = Bound(position, Radius);
    }

    private Vector2 Bound(Vector2 position, float radius)
    {
        // bound stick's position not to be in deadZone and get out of base of joystick
        if (position.sqrMagnitude < (radius * _deadZone) * (radius * _deadZone))
        {
            return Vector2.zero;
        }
        else if (position.sqrMagnitude < radius * radius)
        {
            return position;
        }
        else
        {
            return position.normalized * radius;
        }
    }
}
