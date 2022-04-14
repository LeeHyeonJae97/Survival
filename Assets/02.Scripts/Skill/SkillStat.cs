using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
