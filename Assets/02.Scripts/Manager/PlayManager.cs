using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayMode { Story, Survival }

public class PlayManager : SingletonMonoBehaviour<PlayManager>
{
    public const int INITIAL_COIN = 500;

    // NOTICE :
    // any better idea than using static variable?
    public static PlayMode PlayMode { get; set; }

    public int ElapsedSec { get; private set; }

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
        PoolingManager.Instance.Create("SkillProjectile", "SkillProjectile", 10);

        StartCoroutine(CoElapse());
    }

    private void OnPlayFinished()
    {
        StopAllCoroutines();
    }

    private IEnumerator CoElapse()
    {
        ElapsedSec = 0;

        while (true)
        {
            onElapsedUpdated?.Invoke(ElapsedSec);
            ElapsedSec++;
            yield return WaitForSecondsFactory.Get(1f);
        }
    }
}
