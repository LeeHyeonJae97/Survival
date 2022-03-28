using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] public enum Constraint { None = 0, Fire = 1, Ice = 2, Poison = 4, Wind = 8, Curse = 16 }

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy/Enemy")]
public class Enemy : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public int Hp { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public Constraint Constraint { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public EnemyMovement Movement { get; private set; }
}
