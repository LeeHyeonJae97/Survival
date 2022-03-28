using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteUI : UI
{
    protected override void OnSetActive(bool value)
    {
        Debug.Log($"Open UI {value}");
    }
}
