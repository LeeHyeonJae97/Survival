using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertUI : UI
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _cancelButton;

    protected override void Awake()
    {
        base.Awake();

        _cancelButton.onClick.AddListener(() => SetActive(false));
        DontDestroyOnLoad(gameObject);
    }

    protected override void OnSetActive(bool value)
    {
        if (!value) _messageText.text = null;
    }

    public void Alert(string message)
    {
        if (!Active)
        {
            _messageText.text = message;
            SetActive(true);
        }
    }
}
