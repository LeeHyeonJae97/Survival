using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillHitType { Single, Multiple, NewTarget }

public abstract class SkillHit : ScriptableObject
{
    protected const string FILE_NAME = "Skill Hit ";
    protected const string MENU_NAME = "ScriptableObject/Skill/Hit/";

    [field: SerializeField] public SkillHitType Type { get; protected set; }

    public abstract void Hit_OnTriggerEnter2D(SkillProjectile projectile, Collider2D collision);
    public abstract void Hit_OnTriggerStay2D(SkillProjectile projectile, Collider2D collision);
}

