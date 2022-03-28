using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UITweenSize : UITween
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
        if (!IsTweening && value != IsActive)
        {
            // set start and end size
            Vector2 start = value ? _start : _end;
            Vector2 end = value ? _end : _start;

            // sizing from start to end with duration
            Target.sizeDelta = start;
            Tween = Target.DOSizeDelta(end, directly ? 0 : _duration).SetEase(_ease);
            Tween.onComplete += () => IsActive = value;
        }
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtension.DrawRect(Target.position, _start.x, _start.y);
        GizmosExtension.DrawRect(Target.position, _end.x, _end.y);
    }
}
