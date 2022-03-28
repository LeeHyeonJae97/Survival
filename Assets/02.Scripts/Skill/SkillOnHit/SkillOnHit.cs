using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillOnHitType { None, Despawn, FindNewTarget }

public abstract class SkillOnHit : ScriptableObject
{
    protected const string FILE_NAME = "Skill OnHit ";
    protected const string MENU_NAME = "ScriptableObject/Skill/OnHit/";

    [field: SerializeField] public SkillOnHitType Type { get; protected set; }

    public abstract void OnHit(SkillProjectile projectile);
}
