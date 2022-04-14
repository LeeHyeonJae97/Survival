using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public abstract class UI : MonoBehaviour
{
    public bool IsActive { get; private set; }
    public bool IsActivated { get; private set; }

    [SerializeField] protected bool _showOnAwake;
    private Canvas _canvas;
    private UITween[] _uiTweens;

    public event UnityAction<bool> onSetActive;

    protected abstract void OnSetActive(bool value);

    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _uiTweens = GetComponentsInChildren<UITween>();

        _canvas.worldCamera = MainCamera.Camera;
        _canvas.planeDistance = MainCamera.Camera.transform.position.z * -1;
        _canvas.sortingLayerID = SortingLayer.NameToID("UI");

        UIFactory.Add(this);
        onSetActive += OnSetActive;
    }

    protected virtual void Start()
    {
        SetActive(_showOnAwake, true);
    }

    protected virtual void OnDestroy()
    {
        UIFactory.Remove(this);
        onSetActive -= OnSetActive;
    }

    protected void ResetOnSetActive()
    {
        onSetActive = null;
    }

    public void SetActive(bool value, bool directly = false)
    {
        // NOTICE :
        // if the duration of tweens are different, need to find logest tween

        if (value)
        {
            _canvas.enabled = true;

            // if there is at least one UITween
            if (_uiTweens != null && _uiTweens.Length > 0)
            {
                for (int i = 0; i < _uiTweens.Length; i++)
                {
                    if (!_uiTweens[i].IsTweening && (directly || !_uiTweens[i].IsActive))
                    {
                        _uiTweens[i].Show(true, directly);
                        if (i == 0) _uiTweens[i].Tween.onComplete += () =>
                        {
                            onSetActive?.Invoke(true);
                            IsActivated = true;
                        };
                    }
                }
            }
            // if there's no UITween
            else
            {
                onSetActive?.Invoke(true);
                IsActivated = true;
            }

            IsActive = true;
        }
        else
        {
            // if there is at least one UITween
            if (_uiTweens != null && _uiTweens.Length > 0)
            {
                for (int i = 0; i < _uiTweens.Length; i++)
                {
                    if (!_uiTweens[i].IsTweening && (directly || _uiTweens[i].IsActive))
                    {
                        _uiTweens[i].Show(false, directly);
                        if (i == 0) _uiTweens[i].Tween.onComplete += () =>
                        {
                            _canvas.enabled = false;
                            onSetActive?.Invoke(value);
                            IsActivated = false;
                        };
                    }
                }
            }
            // if there's no UITween
            else
            {
                _canvas.enabled = false;
                onSetActive?.Invoke(value);
                IsActivated = false;
            }

            IsActive = false;
        }
    }
}