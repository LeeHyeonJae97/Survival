using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillStat", menuName = "ScriptableObject/Skill/SkillStat")]
public class SkillStatSO : ScriptableObject
{
    [field: SerializeField] public float Scale { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public float LifeSpan { get; set; }
    [field: SerializeField] public float FlySpeed { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public float Interval { get; private set; }
    [field: SerializeField] public float TargetingRange { get; private set; }
    [field: SerializeField] public float DamagingCooldown { get; private set; }

    // CONTINUE :
    [field: SerializeField] public SkillStat[] Stats;

    // CONTINUE :
    // remove SkillStatList and using SkillStat instead
    [System.Serializable]
    public class SkillStat
    {
        [field: SerializeField] public float Scale { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float LifeSpan { get; set; }
        [field: SerializeField] public float FlySpeed { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public float Interval { get; private set; }
        [field: SerializeField] public float TargetingRange { get; private set; }
        [field: SerializeField] public float DamagingCooldown { get; private set; }
    }
}

[System.Serializable]
public class SkillStatList
{
    [field: SerializeField] public SkillStatSO[] Stats;

    public void OnValidate()
    {
        if (Stats == null)
        {
            Stats = new SkillStatSO[Skill.MAX_REINFORCED];
        }
        else if (Stats.Length != Skill.MAX_REINFORCED)
        {
            SkillStatSO[] replace = new SkillStatSO[Skill.MAX_REINFORCED];

            for (int i = 0; i < Stats.Length && i < Skill.MAX_REINFORCED; i++)
            {
                replace[i] = Stats[i];
            }
            Stats = replace;
        }
    }
}