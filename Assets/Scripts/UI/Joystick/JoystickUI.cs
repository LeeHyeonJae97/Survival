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
    private Transform _base;
    private Transform _stick;
    private bool _inRange;
    private bool _pressed;
    private JoystickEventChannelSO _joystickEventChannel;

    private void Awake()
    {
        _base = transform.GetChild(0).GetChild(0);
        _stick = transform.GetChild(0).GetChild(1);
    }

    private void Update()
    {
        if (_pressed) JoystickEventChannel.OnDrag(_stick.position - _base.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;

        // check pressed position is in range
        _inRange = InRange(eventData.position, _base.position, _radius);

        // if in range, set stick's initial position and invoke event
        if (_inRange)
        {
            _stick.position = eventData.position;
            JoystickEventChannel.OnBeginDrag(_stick.position - _base.position);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // check boundary double click
        if (eventData.clickCount == 2 && !InRange(eventData.position, _base.position, _radius))
        {
            JoystickEventChannel.OnBoundaryDoubleClicked(eventData.position - (Vector2)_base.position);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;

        // reset stick's position and invoke event
        _stick.position = _base.position;
        JoystickEventChannel.OnEndDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // set stick's position
        if (_inRange) _stick.position = Bound(eventData.position, _base.position, _radius);
    }

    private bool InRange(Vector3 position, Vector3 center, float radius)
    {
        // check position is in radius from center
        return (position - center).sqrMagnitude < radius * radius;
    }

    private Vector2 Bound(Vector3 position, Vector3 center, float radius)
    {
        // bound stick's position not to get out of base of joystick
        return InRange(position, center, radius) ? position : center + (position - center).normalized * radius;
    }
}
