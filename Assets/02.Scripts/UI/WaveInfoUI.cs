using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        var stage = WaveManager.GetInstance().Stage;
        _indexText.text = $"{stage.Index}��° ������";
        _nameText.text = stage.Current.Name;
        SetActive(true);
        yield return new WaitUntil(() => IsActivated);
        yield return WaitForSecondsFactory.GetPlayTime(_hideDelay);

        SetActive(false);
    }
}
