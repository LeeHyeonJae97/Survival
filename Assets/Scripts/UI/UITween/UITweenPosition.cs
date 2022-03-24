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

    [SerializeField, MoveTool] private Vector2 _start;
    [SerializeField, MoveTool] private Vector2 _end;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    private RectTransform _target;

    public override void Show(bool value, bool directly)
    {
        // need to wait until last tweening is finished
        if (!IsTweening && value != IsActive)
        {
            // set start and end position
            Vector2 start = value ? _start : _end;
            Vector2 end = value ? _end : _start;

            // NOTICE :
            // if duration is 0, making tween is wasting memory

            Target.position = start;
            Tween = Target.DOMove(end, directly ? 0 : _duration).SetEase(_ease);
            Tween.onComplete += () => IsActive = value;
        }
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtension.DrawRect(Target.rect.min + _start, Target.rect.max + _start);
        GizmosExtension.DrawRect(Target.rect.min + _end, Target.rect.max + _end);
    }
}