using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : SingletonMonoBehaviour<WaveManager>
{
    public WaveSO Wave { get; private set; }
    public int Index { get; private set; }
    public bool IsInfinite { get; set; } = true;
    public List<EnemyPlayer> Enemies { get; private set; } = new List<EnemyPlayer>();

    [SerializeField] private float _range;
    [SerializeField] private float _checkOutOfRangeInterval;

    public event UnityAction<float> onElapsedUpdated;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void OnEnable()
    {
        EventChannelFactory.Get<PlayEventChannelSO>().onPlayStarted += StartNextWave;
    }

    private void OnDisable()
    {
        EventChannelFactory.Get<PlayEventChannelSO>().onPlayStarted -= StartNextWave;
    }

    public void Init()
    {
        // create pool of enemy and start spawning
        PoolingManager.Instance.Create<EnemyPlayer>(amount: 10);
    }

    public void StartNextWave()
    {
        // set wave
        if (IsInfinite)
        {
            Wave = WaveFactory.Random;
        }
        else
        {
            Wave = WaveFactory.Get(Index++);
        }
        Wave.Init();

        // start spawning
        StartCoroutine(CoSpawn());

        // start checking out of range
        StartCoroutine(CoCheckOutOfRange());

        // check elapsed time
        StartCoroutine(CoElapse());

        // resume
        Time.timeScale = 1;

        EventChannelFactory.Get<PlayEventChannelSO>().OnWaveStarted();
    }

    private void FinishWave()
    {
        StopAllCoroutines();

        // pause
        Time.timeScale = 0;

        EventChannelFactory.Get<PlayEventChannelSO>().OnWaveFinished();
    }

    private IEnumerator CoSpawn()
    {
        EnemySpawning spawning;

        while ((spawning = Wave.Next) != null)
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
        float start = Time.time;

        while (true)
        {
            float ratio = (Time.time - start) / Wave.Duration;
            onElapsedUpdated?.Invoke(ratio);

            if (ratio >= 1) FinishWave();

            yield return null;
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
