using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public abstract class UI : MonoBehaviour
{
    public bool IsActive { get; private set; }
    private Canvas Canvas
    {
        get
        {
            if (_canvas == null) _canvas = GetComponent<Canvas>();
            return _canvas;
        }
    }
    private UITween[] UITweens
    {
        get
        {
            if (_uiTweens == null) _uiTweens = GetComponentsInChildren<UITween>();
            return _uiTweens;
        }
    }

    [SerializeField] protected bool _showOnAwake;
    private Canvas _canvas;
    private UITween[] _uiTweens;

    public event UnityAction<bool> onSetActive;

    protected abstract void OnSetActive(bool value);

    protected virtual void Awake()
    {
        UIFactory.Add(this);
        onSetActive += OnSetActive;

        SetActive(_showOnAwake, true, true);
    }

    protected virtual void OnDestroy()
    {
        UIFactory.Remove(this);
        onSetActive -= OnSetActive;
    }

    public void SetActive(bool value, bool directly = false, bool forcibly = false)
    {
        // NOTICE :
        // if the duration of tweens are different, need to find logest tween

        if (value)
        {
            Canvas.enabled = true;

            // if there is at least one UITween
            if (UITweens.Length > 0)
            {
                for (int i = 0; i < UITweens.Length; i++)
                {
                    if (!UITweens[i].IsTweening && (forcibly || !UITweens[i].IsActive))
                    {
                        UITweens[i].Show(true, directly);
                        if (i == 0) UITweens[i].Tween.onComplete += () => onSetActive?.Invoke(true);
                    }
                }
            }
            // if there's no UITween
            else
            {
                onSetActive?.Invoke(true);
            }

            IsActive = true;
        }
        else
        {
            // if there is at least one UITween
            if (UITweens.Length > 0)
            {
                for (int i = 0; i < UITweens.Length; i++)
                {
                    // CONTINUE :
                    Debug.Log($"{!UITweens[i].IsTweening} {UITweens[i].IsActive}");

                    if (!UITweens[i].IsTweening && (forcibly ||UITweens[i].IsActive))
                    {
                        UITweens[i].Show(false, directly);
                        if (i == 0) UITweens[i].Tween.onComplete += () =>
                        {
                            Canvas.enabled = false;
                            onSetActive?.Invoke(value);
                        };
                    }
                }
            }
            // if there's no UITween
            else
            {
                Canvas.enabled = false;
                onSetActive?.Invoke(value);
            }

            IsActive = false;
        }
    }
}