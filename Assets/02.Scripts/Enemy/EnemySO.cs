using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] public enum Constraint { None = 0, Fire = 1, Ice = 2, Poison = 4, Wind = 8, Curse = 16 }

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float Size { get; private set; }
    [field: SerializeField] public Stat[] Stats { get; private set; }
    [field: SerializeField] public Constraint Constraint { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public EnemyMovement Movement { get; private set; }

    private void OnValidate()
    {
        // enemy does not need 'CooldownReduce'
        int length = System.Enum.GetValues(typeof(StatType)).Length - 1;

        if (Stats == null)
        {
            Stats = new Stat[length];
        }
        else if (Stats.Length != length)
        {
            Stat[] replace = new Stat[length];
            for (int i = 0; i < Stats.Length && i < length; i++)
                replace[i] = Stats[i];
            Stats = replace;
        }
    }
}
