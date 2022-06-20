using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayMode { Story, Survival }

public class PlayManager : SingletonMonoBehaviour<PlayManager>
{
    public const int INITIAL_COIN = 500;

    public int ElapsedSec { get; private set; } = 0;

    public event UnityAction<int> onElapsedUpdated;

    private void OnEnable()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayStarted += OnPlayStarted;
        channel.OnPlayFinished += OnPlayFinished;
        channel.OnWaveStarted += OnWaveStarted;
        channel.OnWaveFinished += OnWaveFinished;
        channel.OnPaused += OnPaused;
        channel.OnResumed += OnResumed;
    }

    private void OnDisable()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayStarted -= OnPlayStarted;
        channel.OnPlayFinished -= OnPlayFinished;
        channel.OnWaveStarted -= OnWaveStarted;
        channel.OnWaveFinished -= OnWaveFinished;
        channel.OnPaused -= OnPaused;
        channel.OnResumed -= OnResumed;
    }

    private void OnPlayStarted()
    {
        PlayTime.timeScale = 1;
        StartCoroutine(CoElapse());
    }

    private void OnPlayFinished()
    {
        PlayTime.timeScale = 0;
        StopAllCoroutines();
    }

    private void OnWaveStarted()
    {
        PlayTime.timeScale = 1;
    }

    private void OnWaveFinished()
    {
        PlayTime.timeScale = 0;
    }

    private void OnPaused()
    {
        PlayTime.timeScale = 0;
    }

    private void OnResumed()
    {
        PlayTime.timeScale = 1;
    }

    private IEnumerator CoElapse()
    {
        ElapsedSec = 0;

        while (true)
        {
            onElapsedUpdated?.Invoke(ElapsedSec);
            ElapsedSec++;
            yield return WaitForSecondsFactory.GetPlayTime(1f);
        }
    }
}
