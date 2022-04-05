using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UI
{
    [SerializeField] private Toggle _muteToggle;
    [SerializeField] private Toggle _vibrationToggle;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;

    protected override void Awake()
    {
        base.Awake();

        _muteToggle.onValueChanged.AddListener(OnMuteToggleValueChanged);
        _vibrationToggle.onValueChanged.AddListener(OnVibrationToggleValueChanged);
        _saveButton.onClick.AddListener(OnClickSaveButton);
        _loadButton.onClick.AddListener(OnClickLoadButton);
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            UserSO user = GameManager.Instance.User;

            _muteToggle.isOn = !user.Mute;
            _vibrationToggle.isOn = user.Vibration;
        }
    }

    private void OnMuteToggleValueChanged(bool value)
    {
        GameManager.Instance.User.Mute = !value;
    }

    private void OnVibrationToggleValueChanged(bool value)
    {
        GameManager.Instance.User.Vibration = value;
    }

    private void OnClickSaveButton()
    {

    }

    private void OnClickLoadButton()
    {

    }
}
