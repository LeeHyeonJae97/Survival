using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public const string DIR_PATH = "EnemyData";
    public const string FILE_PATH = "enemyData.json";

    [field: SerializeField] public Enemy[] Enemies { get; private set; }

    public EnemyData()
    {
        Enemies = new Enemy[EnemyFactory.Count];
        for (int i = 0; i < Enemies.Length; i++) Enemies[i] = new Enemy();
    }

    public EnemyData(Enemy[] items)
    {
        this.Enemies = items;
    }
}
