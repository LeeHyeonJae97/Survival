using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertUI : UI
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void OnSetActive(bool value)
    {

    }
}
