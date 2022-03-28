using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UITweenAlpha : UITween
{
    private CanvasGroup Target
    {
        get
        {
            if (_target == null) _target = GetComponent<CanvasGroup>();
            return _target;
        }
    }

    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    private CanvasGroup _target;

    public override void Show(bool value, bool directly)
    {
        // need to wait until last tweening is finished
        if (!IsTweening && value != IsActive)
        {
            // set start and end position
            float start = value ? 0 : 1;
            float end = value ? 1 : 0;

            // fade with duration
            Target.alpha = start;
            Tween = Target.DOFade(end, directly ? 0 : _duration).SetEase(_ease);
            Tween.onComplete += () => IsActive = value;
        }
    }
}
