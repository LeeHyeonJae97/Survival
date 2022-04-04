using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : SingletonMonoBehaviour<PlayManager>
{
    public event UnityAction<int> onElapsedUpdated;

    private void OnEnable()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.onPlayStarted += OnPlayStarted;
        channel.onPlayFinished += OnPlayFinished;
    }

    private void OnDisable()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.onPlayStarted -= OnPlayStarted;
        channel.onPlayFinished -= OnPlayFinished;
    }

    private void OnPlayStarted()
    {
        StartCoroutine(CoElapse());
    }

    private void OnPlayFinished()
    {
        StopAllCoroutines();
    }

    private IEnumerator CoElapse()
    {
        int sec = 0;

        while (true)
        {
            onElapsedUpdated?.Invoke(sec);
            sec++;
            yield return WaitForSecondsFactory.Get(1f);
        }
    }
}
