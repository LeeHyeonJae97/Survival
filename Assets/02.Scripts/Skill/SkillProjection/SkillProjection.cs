using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillProjectionType { Straight, Sin, Spiral, Boomerang, PingPong }

public abstract class SkillProjection : ScriptableObject
{
    public const string FILE_NAME = "Skill Projection ";
    public const string MENU_NAME = "ScriptableObject/Skill/Projection/";

    [field: SerializeField] public SkillProjectionType Type { get; protected set; }

    public abstract void Projection_Start(SkillProjectile projectile);
    public abstract void Projection_Update(SkillProjectile projectile);
}
