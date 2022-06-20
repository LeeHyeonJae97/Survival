using DG.Tweening;
using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UITweenPosition : UITween
{
    public RectTransform Target
    {
        get
        {
            if (_target == null) _target = GetComponent<RectTransform>();
            return _target;
        }
    }

    [SerializeField] private Vector2 _start;
    [SerializeField] private Vector2 _end;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    private RectTransform _target;

    public override void Show(bool value, bool directly)
    {
        // need to wait until last tweening is finished
        if (!IsTweening && (directly || value != IsActive))
        {
            // set start and end position
            Vector2 start = MainCamera.Camera.WorldToScreenPoint(value ? _start : _end);
            Vector2 end = MainCamera.Camera.WorldToScreenPoint(value ? _end : _start);

            // NOTICE :
            // if duration is 0, making tween is wasting memory

            Target.position = start;
            Tween = Target.DOAnchorPos(end, directly ? 0 : _duration).SetEase(_ease);
            Tween.onComplete += () => IsActive = value;
        }
    }
}
