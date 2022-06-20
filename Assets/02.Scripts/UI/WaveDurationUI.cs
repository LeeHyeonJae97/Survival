using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDurationUI : UI
{
    [SerializeField] private Image _durationImage;
    [SerializeField] private Image _durationFillImage;

    protected override void OnSetActive(bool value)
    {
        if (value) 
        {
            WaveManager.GetInstance().onElapsedUpdated += OnDurationUpdated;
        }
        else
        {
            WaveManager.GetInstance().onElapsedUpdated -= OnDurationUpdated;
        }
    }

    private void OnDurationUpdated(float ratio)
    {
        _durationFillImage.fillAmount = ratio;
    }
}
