using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
