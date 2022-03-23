using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy/Enemy")]
public class Enemy : ScriptableObject
{
    [field: SerializeField] public int HP { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public EnemyMovement Movement { get; private set; }
}
