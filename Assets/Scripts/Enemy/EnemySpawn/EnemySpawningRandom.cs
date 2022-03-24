using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Random", menuName = MENU_NAME + "Random")]
public class EnemySpawningRandom : EnemySpawning
{
    [field: SerializeField] public int Amount { get; private set; }

    private void OnValidate()
    {
        Type = EnemySpawningType.Random;   
    }

    public override EnemyPlayer[] Spawn()
    {
        EnemyPlayer[] enemies = new EnemyPlayer[Amount];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = PoolingManager.Instance.Spawn<EnemyPlayer>();
            enemies[i].transform.position = MainCamera.OrthographicBounds(_boundsOffset).OnRandom();
            enemies[i].Init(_enemies[Random.Range(0, _enemies.Length)]);
        }
        return enemies;
    }
}
