using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
{
    [SerializeField] private float _range;
    [SerializeField] private float _checkOutOfRangeInterval;
    [SerializeField] private WaveInfo _waveInfo;
    private List<EnemyPlayer> _enemies = new List<EnemyPlayer>();

    protected override void Awake()
    {
        base.Awake();

        // DEPRECATED
        Init();
    }

    public void Init()
    {
        // initialize wave info
        _waveInfo.Init();

        // create pool of enemy and start spawning
        PoolingManager.Instance.Create<EnemyPlayer>(amount: 10);
        StartCoroutine(CoSpawn());

        // start checking out of range
        StartCoroutine(CheckOutOfRange());
    }

    private IEnumerator CoSpawn()
    {
        EnemySpawning spawning;

        while ((spawning = _waveInfo.Next) != null)
        {
            float elapsed = 0;

            while (elapsed < spawning.Duration)
            {
                _enemies.AddRange(spawning.Spawn());
                yield return WaitForSecondsFactory.Get(spawning.Interval);

                elapsed += spawning.Interval;
            }
        }
    }

    public void Despawn(EnemyPlayer enemy)
    {
        _enemies.Remove(enemy);

        PoolingManager.Instance.Despawn(enemy);
    }

    private IEnumerator CheckOutOfRange()
    {
        while (true)
        {
            // check enemy is out of range (too far from player)
            for (int i = 0; i < _enemies.Count; i++)
            {
                if ((_enemies[i].transform.position - Player.Instance.transform.position).sqrMagnitude >= _range * _range)
                {
                    // decrease 'i' not to skip erased index
                    _enemies[i--].Die();
                }
            }

            yield return WaitForSecondsFactory.Get(_checkOutOfRangeInterval);
        }
    }

    public EnemyPlayer Closest(Vector3 position)
    {
        if (_enemies.Count == 0) return null;

        float dstThreshold = 1f;
        float minDst = float.MaxValue;
        EnemyPlayer target = null;

        for (int i = 0; i < _enemies.Count; i++)
        {
            float dst = _enemies[i].transform.position.SqrDst(position);
            if (dstThreshold < dst && dst < minDst)
            {
                minDst = dst;
                target = _enemies[i];
            }
        }
        return target;
    }

    public EnemyPlayer Random()
    {
        return _enemies.Count == 0 ? null : _enemies[UnityEngine.Random.Range(0, _enemies.Count)];
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) GizmosExtension.DrawCircle(Player.Instance.transform.position, _range);
    }
}
