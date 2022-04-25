using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : SingletonMonoBehaviour<WaveManager>
{
    [field: SerializeField] public WaveSO InitialWave { get; private set; }
    public WaveBundleSO WaveBundle
    {
        get
        {
            if (_waveBundle == null) _waveBundle = Resources.Load<WaveBundleSO>($"Wave/WaveBundle {PlayManager.PlayMode}");
            return _waveBundle;
        }
    }
    public WaveSO Next
    {
        get
        {
            Current = WaveBundle.Next;
            return Current;
        }
    }
    public WaveSO Current
    {
        get
        {
            if (_current == null) _current = InitialWave;
            return _current;
        }

        private set { _current = value; }
    }
    public int Index { get { return WaveBundle.Index; } }
    public List<EnemyPlayer> Enemies { get; private set; } = new List<EnemyPlayer>();

    [SerializeField] private float _range;
    [SerializeField] private float _checkOutOfRangeInterval;
    private WaveBundleSO _waveBundle;
    private WaveSO _current;

    public event UnityAction<float> onElapsedUpdated;

    private void OnDestroy()
    {
        WaveBundle.Reset();
    }

    private void OnEnable()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayStarted += channel.FinishWave;
        channel.OnWaveStarted += OnWaveStarted;
        channel.OnWaveFinished += OnWaveFinished;
    }

    private void OnDisable()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayStarted -= channel.FinishWave;
        channel.OnWaveStarted -= OnWaveStarted;
        channel.OnWaveFinished -= OnWaveFinished;
    }

    private void OnWaveStarted()
    {
        // start spawning
        StartCoroutine(CoSpawn());

        // start checking out of range
        StartCoroutine(CoCheckOutOfRange());

        // check elapsed time
        StartCoroutine(CoElapse());
    }

    private void OnWaveFinished()
    {
        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayStarted -= channel.FinishWave;

        // stop spawning and checking enemy out of range and duration
        StopAllCoroutines();
    }

    private IEnumerator CoSpawn()
    {
        WaveSO wave = Next;
        EnemySpawning spawning;

        while ((spawning = wave.Next) != null)
        {
            float elapsed = 0;

            while (elapsed < spawning.Duration)
            {
                Enemies.AddRange(spawning.Spawn());
                yield return WaitForSecondsFactory.Get(spawning.Interval);

                elapsed += spawning.Interval;
            }
        }
    }

    public void Despawn(EnemyPlayer enemy)
    {
        Enemies.Remove(enemy);

        PoolingManager.Instance.Despawn(enemy);
    }

    private IEnumerator CoCheckOutOfRange()
    {
        while (true)
        {
            // check enemy is out of range (too far from player)
            for (int i = 0; i < Enemies.Count; i++)
            {
                if ((Enemies[i].transform.position - Player.Instance.transform.position).sqrMagnitude >= _range * _range)
                {
                    // decrease 'i' not to skip erased index
                    Enemies[i--].Despawn();
                }
            }

            yield return WaitForSecondsFactory.Get(_checkOutOfRangeInterval);
        }
    }

    private IEnumerator CoElapse()
    {
        float duration = WaveBundle.Current.Duration;
        float remainDuration = duration;

        while (true)
        {
            float ratio = 1 - (remainDuration / duration);
            onElapsedUpdated?.Invoke(ratio);
            if (ratio >= 1) EventChannelFactory.Get<PlayEventChannelSO>().FinishWave();
            yield return null;

            remainDuration -= PlayTime.deltaTime;
        }
    }

    public EnemyPlayer Closest(Vector3 position, float range)
    {
        if (Enemies.Count == 0) return null;

        float minDst = float.MaxValue;
        EnemyPlayer target = null;

        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].transform.position == position) continue;

            float dst = Enemies[i].transform.position.SqrDst(position);

            if (dst < range * range && dst < minDst)
            {
                minDst = dst;
                target = Enemies[i];
            }
        }
        return target;
    }

    public EnemyPlayer Random(Vector3 position, float range)
    {
        if (Enemies.Count == 0) return null;

        List<EnemyPlayer> enemies = new List<EnemyPlayer>();

        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].transform.position.SqrDst(position) < range * range)
            {
                enemies.Add(Enemies[i]);
            }
        }
        return enemies.Count == 0 ? null : enemies[UnityEngine.Random.Range(0, enemies.Count)];
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) GizmosExtension.DrawCircle(Player.Instance.transform.position, _range);
    }
}
