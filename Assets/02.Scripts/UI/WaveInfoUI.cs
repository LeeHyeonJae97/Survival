using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoUI : UI
{
    [SerializeField] private TextMeshProUGUI _indexText;
    [SerializeField] private TextMeshProUGUI _nameText;

    protected override void Awake()
    {
        base.Awake();

        EventChannelFactory.Get<PlayEventChannelSO>().onWaveStarted += OnWaveStarted;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventChannelFactory.Get<PlayEventChannelSO>().onWaveStarted -= OnWaveStarted;
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
        WaveSO wave = WaveManager.Instance.Wave;
        _indexText.text = $"{WaveManager.Instance.Index}";
        _nameText.text = wave.Name;
        SetActive(true);
        yield return new WaitUntil(() => IsActivated);

        SetActive(false);
    }
}
