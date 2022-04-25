using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoUI : UI
{
    [SerializeField] private float _hideDelay;
    [SerializeField] private TextMeshProUGUI _indexText;
    [SerializeField] private TextMeshProUGUI _nameText;

    protected override void Awake()
    {
        base.Awake();

        EventChannelFactory.Get<PlayEventChannelSO>().OnWaveStarted += OnWaveStarted;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventChannelFactory.Get<PlayEventChannelSO>().OnWaveStarted -= OnWaveStarted;
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnWaveStarted()
    {
        StartCoroutine(CoOnWaveStarted());
    }

    private IEnumerator CoOnWaveStarted()
    {
        _indexText.text = $"{WaveManager.Instance.Index}¹øÂ° ¸ô·Á¿È";
        _nameText.text = WaveManager.Instance.Current.Name;
        SetActive(true);
        yield return new WaitUntil(() => IsActivated);
        yield return WaitForSecondsFactory.Get(_hideDelay);

        SetActive(false);
    }
}
