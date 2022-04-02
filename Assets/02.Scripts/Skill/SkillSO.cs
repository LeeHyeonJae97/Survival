using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill ", menuName = "ScriptableObject/Skill/Skill")]
public class SkillSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public SkillStat[] Stats { get; private set; }
    [field: SerializeField] public SkillProperty Property { get; private set; }
    [field: SerializeField] public SkillInvocation Invocation { get; private set; }
    [field: SerializeField] public SkillTargeting Targeting { get; private set; }
    [field: SerializeField] public SkillProjection Projection { get; private set; }
    [field: SerializeField] public SkillHit Hit { get; private set; }
    [field: SerializeField] public SkillOnHit OnHit { get; private set; }
}

[System.Serializable]
public struct SkillStat
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

    #region Equality
    public override bool Equals(object obj)
    {
        return obj is SkillStat stat && Scale == stat.Scale && Damage == stat.Damage && Cooldown == stat.Cooldown
            && LifeSpan == stat.LifeSpan && FlySpeed == stat.FlySpeed
            && Amount == stat.Amount && Interval == stat.Interval;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(SkillStat lhs, SkillStat rhs)
    {
        if (lhs.Equals(default))
        {
            if (rhs.Equals(default))
            {
                return true;
            }

            // only the left side is null
            return false;
        }
        return lhs.Equals(rhs);
    }

    public static bool operator !=(SkillStat lhs, SkillStat rhs)
    {
        return !(lhs == rhs);
    }
    #endregion
}