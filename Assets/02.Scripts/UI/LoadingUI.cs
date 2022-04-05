using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingUI : UI
{
    [SerializeField] private TextMeshProUGUI _titleText;

    public string Title { set { _titleText.text = value; } }

    protected override void OnSetActive(bool value)
    {
        if (!value) ResetOnSetActive();
    }
}
