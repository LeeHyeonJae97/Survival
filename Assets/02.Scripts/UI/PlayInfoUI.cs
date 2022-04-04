using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayInfoUI : UI
{
    [SerializeField] private TextMeshProUGUI _elapsedText;

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            PlayManager.Instance.onElapsedUpdated += OnElapsedUpdated;
        }
        else
        {
            PlayManager.Instance.onElapsedUpdated -= OnElapsedUpdated;
        }
    }

    private void OnElapsedUpdated(int elapsed)
    {
        _elapsedText.text = $"{elapsed}";
    }
}
