using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill ", menuName = "ScriptableObject/Skill/Skill")]
public class Skill : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public SkillStat Stat { get; private set; }
    [field: SerializeField] public SkillProperty Property { get; private set; }
    [field: SerializeField] public SkillInvocation Invocation { get; private set; }
    [field: SerializeField] public SkillTargeting Targeting { get; private set; }
    [field: SerializeField] public SkillProjection Projection { get; private set; }
    [field: SerializeField] public SkillHit Hit { get; private set; }

    //public Skill(SkillProperty property, SkillInvocation invocation, SkillTargeting targeting, SkillProjection projection, SkillHit hit)
    //{
    //    // save
    //    Property = property;
    //    Invocation = invocation;
    //    Targeting = targeting;
    //    Projection = projection;
    //    Hit = hit;
    //}

    public void Invoke()
    {
        Player.Instance.StartCoroutine(CoInvoke());
    }

    public void Reinforce()
    {
        Stat.Reinforce();
    }

    private IEnumerator CoInvoke()
    {
        while (true)
        {
            // wait for cooldown
            yield return WaitForSecondsFactory.Get(Stat.Cooldown);

            // invoke skill
            Player.Instance.StartCoroutine(Invocation?.CoInvoke(this));
        }
    }
}

[System.Serializable]
public struct SkillStat
{
    //public static SkillStat Random
    //{
    //    get
    //    {
    //        SkillStat stat = new SkillStat();
    //        stat.Damage = UnityEngine.Random.Range(1, 2);
    //        stat.Cooldown = UnityEngine.Random.Range(1f, 2f);
    //        stat.LifeSpan = UnityEngine.Random.Range(10f, 10f);
    //        stat.FlySpeed = UnityEngine.Random.Range(8f, 10f);
    //        stat.Amount = UnityEngine.Random.Range(1, 5);
    //        stat.Interval = UnityEngine.Random.Range(0, 1f);
    //        return stat;
    //    }
    //}

    [field: SerializeField] public float Scale { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public float LifeSpan { get; set; }
    [field: SerializeField] public float FlySpeed { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public float Interval { get; private set; }

    public void Reinforce()
    {
        Scale += Random.Range(0, 1);
        Damage += Random.Range(0, 1);
        Cooldown += Random.Range(0, 1);
        LifeSpan += Random.Range(0, 1);
        FlySpeed += Random.Range(0, 1);
        Amount += Random.Range(0, 1);
        Interval += Random.Range(0, 1);
    }

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