using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemySpawningType { Random, Fixed }

public abstract class EnemySpawning : ScriptableObject
{
    protected const string FILE_NAME = "Enemy Spawning ";
    protected const string MENU_NAME = "ScriptableObject/Spawn/Spawning/";

    [field: SerializeField] public EnemySpawningType Type { get; protected set; }
    [field: SerializeField] public float Duration { get; protected set; }
    [field: SerializeField] public float Interval { get; protected set; }

    [SerializeField] protected float _boundsOffset;
    [SerializeField] protected Enemy[] _enemies;

    public abstract EnemyPlayer[] Spawn();
}
