using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayInfoUI : UI
{
    public Vector2 CoinImagePosition => _coinImage.transform.position;

    [SerializeField] private Image _coinImage;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _elapsedText;

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            PlayManager.Instance.onElapsedUpdated += OnElapsedUpdated;
            Player.Instance.onCoinUpdated += OnCoinUpdated;
        }
        else
        {
            PlayManager.Instance.onElapsedUpdated -= OnElapsedUpdated;
            Player.Instance.onCoinUpdated += OnCoinUpdated;
        }
    }

    private void OnElapsedUpdated(int elapsed)
    {
        _elapsedText.text = $"{elapsed / 60}:{elapsed % 60}";
    }

    private void OnCoinUpdated(int coin)
    {
        _coinText.text = $"{coin}";
    }
}
