using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : UI
{
    [SerializeField] private Button _pauseButton;

    protected override void Awake()
    {
        base.Awake();

        _pauseButton.onClick.AddListener(OnClickPauseButton);
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnClickPauseButton()
    {
        EventChannelFactory.Get<PlayEventChannelSO>().Pause();
    }
}
