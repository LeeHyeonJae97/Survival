using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
{
    [SerializeField] private float _spawnBoundsOffset;

    private My.Camera Camera { get { if (_camera == null) _camera = new My.Camera(UnityEngine.Camera.main); return _camera; } }

    private List<Enemy> _enemies = new List<Enemy>();
    private My.Camera _camera;
    // NOTICE :
    // need to change 'StageInfo'
    private float _spawnCooldown = .5f;

    protected override void Awake()
    {
        base.Awake();

        // DEPRECATED :
        Spawn();
    }

    public void Spawn()
    {
        PoolingManager.Instance.Create("Enemy", "Enemy", 10);

        StartCoroutine(CoSpawn());
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
            enemy.transform.position = Camera.RandomPointOnBounds(_spawnBoundsOffset);
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
}
