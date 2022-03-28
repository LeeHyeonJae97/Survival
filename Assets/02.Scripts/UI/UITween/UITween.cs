using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITween : MonoBehaviour
{
    public bool IsActive { get; protected set; }
    public bool IsTweening => Tween != null && Tween.IsActive();
    public float Duration => Tween == null ? -1 : Tween.Duration();
    public Tweener Tween { get; protected set; }

    public abstract void Show(bool value, bool directly);
}
