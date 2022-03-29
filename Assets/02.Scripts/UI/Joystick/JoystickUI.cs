using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
{
    private JoystickEventChannelSO JoystickEventChannel
    {
        get
        {
            if (_joystickEventChannel == null) _joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
            return _joystickEventChannel;
        }
    }

    [SerializeField] private float _radius;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _base;
    [SerializeField] private RectTransform _stick;
    private bool _inRange;
    private bool _pressed;
    private JoystickEventChannelSO _joystickEventChannel;

    private void Update()
    {
        if (_pressed) JoystickEventChannel.OnDrag(_stick.anchoredPosition - _base.anchoredPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;

        Vector3 position = eventData.position - _base.anchoredPosition;

        Debug.Log(position);

        // check pressed position is in range
        _inRange = InRange(position, _base.anchoredPosition, _radius);

        // if in range, set stick's initial position and invoke event
        if (_inRange)
        {
            _stick.anchoredPosition = position;
            JoystickEventChannel.OnBeginDrag(_stick.anchoredPosition - _base.anchoredPosition);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 position = eventData.position - _base.anchoredPosition;

        // check boundary double click
        if (eventData.clickCount == 2 && !InRange(position, _base.anchoredPosition, _radius))
        {
            JoystickEventChannel.OnBoundaryDoubleClicked(position - _base.anchoredPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;

        // reset stick's position and invoke event
        _stick.anchoredPosition = _base.anchoredPosition;
        JoystickEventChannel.OnEndDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = eventData.position - _base.anchoredPosition;

        // set stick's position
        if (_inRange) _stick.anchoredPosition = Bound(position, _base.anchoredPosition, _radius);
    }

    private bool InRange(Vector2 position, Vector2 center, float radius)
    {
        // check position is in radius from center
        return (position - center).sqrMagnitude < radius * radius;
    }

    private Vector3 Bound(Vector2 position, Vector2 center, float radius)
    {
        // bound stick's position not to get out of base of joystick
        return InRange(position, center, radius) ? position : center + (position - center).normalized * radius;
    }
}
