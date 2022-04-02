using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmUI : UI
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    public event UnityAction onAccepted;

    protected override void Awake()
    {
        base.Awake();

        _acceptButton.onClick.AddListener(() => onAccepted?.Invoke());
        _cancelButton.onClick.AddListener(() => SetActive(false));
    }

    protected override void OnSetActive(bool value)
    {
        if (!value)
        {
            _messageText.text = null;
            onAccepted = null;
        }
    }

    public void Confirm(string message, UnityAction onAccepted)
    {
        if (!IsActive)
        {
            _messageText.text = message;
            this.onAccepted += onAccepted;
            this.onAccepted += () => SetActive(false);
            SetActive(true);
        }
    }
}
