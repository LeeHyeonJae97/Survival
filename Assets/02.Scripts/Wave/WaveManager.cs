using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : SingletonMonoBehaviour<WaveManager>
{
    public int StageId { get; set; }
    public StageSO Stage
    {
        get
        {
            if (_stage == null) _stage = StageFactory.Get(StageId).Info;
            return _stage;
        }
    }
    public List<EnemyPlayer> Enemies { get; private set; } = new List<EnemyPlayer>();

    [SerializeField] private float _range;
    [SerializeField] private float _checkOutOfRangeInterval;
    [SerializeField] private QuadtreeSettings _quadtreeSettings;
    private Quadtree<EnemyPlayer> _quadtree;
    private List<EnemyPlayer> _neighbors;
    private StageSO _stage;

    public event UnityAction<float> onElapsedUpdated;

    private void Start()
    {
        _quadtree = new Quadtree<EnemyPlayer>(_quadtreeSettings.Center, _quadtreeSettings.Size, _quadtreeSettings.MinDepth);
        _neighbors = new List<EnemyPlayer>();
    }

    private void OnDestroy()
    {
        Stage.Reset();
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

    private void Update()
    {
        UpdateQuadTree();
        MoveEnemies();

        void UpdateQuadTree()
        {
            _quadtree.Reset();

            foreach (var enemy in Enemies)
            {
                _quadtree.Add(enemy);
            }
        }

        void MoveEnemies()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Move(GetNeighborEnemies(Enemies[i]));
            }
        }
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
        // stop spawning and checking enemy out of range and duration
        StopAllCoroutines();
    }

    private IEnumerator CoSpawn()
    {
        WaveSO wave = Stage.Next;
        EnemySpawning spawning;

        while ((spawning = wave.Next) != null)
        {
            float elapsed = 0;

            while (elapsed < spawning.Duration)
            {
                Enemies.AddRange(spawning.Spawn());
                yield return WaitForSecondsFactory.GetPlayTime(spawning.Interval);

                elapsed += spawning.Interval;
            }
        }
    }

    public List<EnemyPlayer> GetNeighborEnemies(EnemyPlayer enemy)
    {
        _neighbors.Clear();
        _quadtree.GetColliding(_neighbors, enemy.transform.position, 1f);

        return _neighbors;
    }

    public void Despawn(EnemyPlayer enemy)
    {
        Enemies.Remove(enemy);

        PoolingManager.GetInstance().Despawn(enemy);
    }

    private IEnumerator CoCheckOutOfRange()
    {
        while (true)
        {
            // check enemy is out of range (too far from player)
            for (int i = 0; i < Enemies.Count; i++)
            {
                if ((Enemies[i].transform.position - Player.GetInstance().transform.position).sqrMagnitude >= _range * _range)
                {
                    // decrease 'i' not to skip erased index
                    Enemies[i--].Despawn();
                }
            }

            yield return WaitForSecondsFactory.GetPlayTime(_checkOutOfRangeInterval);
        }
    }

    private IEnumerator CoElapse()
    {
        float duration = Stage.Current.Duration;
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
        if (Application.isPlaying) Gizmos.DrawWireCircle(Player.GetInstance().transform.position, _range);
    }
}
