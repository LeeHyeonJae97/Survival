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
    [SerializeField] private Transform _base;
    [SerializeField] private Transform _stick;
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
        if (_pressed) JoystickEventChannel.OnDrag(_canvas.InverseTransformDirection(_stick.position - _base.position));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;

        Vector3 position = _canvas.TransformScreenPoint(eventData.position);

        // check pressed position is in range
        _inRange = InRange(position, _base.position, _radius);

        // if in range, set stick's initial position and invoke event
        if (_inRange)
        {
            _stick.position = position;
            JoystickEventChannel.OnBeginDrag(_canvas.InverseTransformDirection( _stick.position - _base.position));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 position = _canvas.TransformScreenPoint(eventData.position);

        // check boundary double click
        if (eventData.clickCount == 2 && !InRange(position, _base.position, _radius))
        {
            JoystickEventChannel.OnBoundaryDoubleClicked(_canvas.InverseTransformDirection(position - _base.position));
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
        Vector3 position = _canvas.TransformScreenPoint(eventData.position);

        // set stick's position
        if (_inRange) _stick.position = Bound(position, _base.position, _radius);
    }

    private bool InRange(Vector3 position, Vector3 center, float radius)
    {
        // check position is in radius from center
        return (position - center).sqrMagnitude < radius * radius;
    }

    private Vector3 Bound(Vector3 position, Vector3 center, float radius)
    {
        // bound stick's position not to get out of base of joystick
        return InRange(position, center, radius) ? position : center + (position - center).normalized * radius;
    }
}
