using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public abstract class UI : MonoBehaviour
{
    private Canvas Canvas
    {
        get
        {
            if (_canvas == null) _canvas = GetComponent<Canvas>();
            return _canvas;
        }
    }

    private Canvas _canvas;

    public event UnityAction<bool> onSetActive;

    protected abstract void OnSetActive(bool value);

    protected virtual void Awake()
    {
        Canvas.enabled = false;

        UIFactory.Add(this);
        onSetActive += OnSetActive;
    }

    protected virtual void OnDestroy()
    {
        UIFactory.Remove(this);
        onSetActive -= OnSetActive;
    }

    public void SetActive(bool value)
    {
        Canvas.enabled = value;
        onSetActive?.Invoke(value);
    }
}
