using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
{
    [SerializeField] private float _range;
    [SerializeField] private float _checkOutOfRangeInterval;
    [SerializeField] private float _spawnBoundsOffset;

    private CameraBounds CameraBounds { get { if (_cameraBounds == null) _cameraBounds = new CameraBounds(Camera.main); return _cameraBounds; } }

    private List<Enemy> _enemies = new List<Enemy>();
    private CameraBounds _cameraBounds;
    // NOTICE :
    // need to change 'StageInfo'
    private float _spawnCooldown = .5f;

    protected override void Awake()
    {
        base.Awake();

        // DEPRECATED
        Init();
    }

    public void Init()
    {
        // create pool of enemy and start spawning
        PoolingManager.Instance.Create("Enemy", "Enemy", 10);
        StartCoroutine(CoSpawn());

        // start checking out of range
        StartCoroutine(CheckOutOfRange());
    }

    private IEnumerator CoSpawn()
    {
        //Enemy enemy = PoolingManager.Instance.Spawn<Enemy>("Enemy");
        //enemy.transform.position = Camera.RandomPointOnBounds(_spawnBoundsOffset);
        //enemy.Init();

        //_enemies.Add(enemy);

        //yield return null;

        while (true)
        {
            Enemy enemy = PoolingManager.Instance.Spawn<Enemy>("Enemy");
            enemy.transform.position = CameraBounds.RandomPointOnBounds(_spawnBoundsOffset);
            enemy.Init();

            _enemies.Add(enemy);

            yield return WaitForSecondsFactory.Get(_spawnCooldown);
        }
    }

    public void Despawn(Enemy enemy)
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
                    _enemies[i].Die();
                }
            }

            yield return WaitForSecondsFactory.Get(_checkOutOfRangeInterval);
        }
    }

    public Enemy Closest(Vector3 position)
    {
        if (_enemies.Count == 0) return null;

        float dstThreshold = 1f;
        float minDst = float.MaxValue;
        Enemy target = null;

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

    public Enemy Random()
    {
        return _enemies.Count == 0 ? null : _enemies[UnityEngine.Random.Range(0, _enemies.Count)];
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtension.DrawCircle(Camera.main.transform.position, _range);
    }
}
