using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Fixed", menuName = MENU_NAME + "Fixed")]
public class EnemySpawningFixed : EnemySpawning
{
    [SerializeField, MoveTool] private Vector2[] _points;

    private void OnValidate()
    {
        Type = EnemySpawningType.Fixed;
    }

    public override EnemyPlayer[] Spawn()
    {
        EnemyPlayer[] enemies = new EnemyPlayer[_points.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = PoolingManager.GetInstance().Spawn<EnemyPlayer>();
            enemies[i].transform.position = (Vector2)MainCamera.Transform.position + _points[i];
            enemies[i].Init(_enemies[Random.Range(0, _enemies.Length)]);
        }
        return enemies;
    }
}
