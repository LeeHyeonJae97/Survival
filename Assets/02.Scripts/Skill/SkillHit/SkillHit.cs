using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillHitType { Single, Multiple, NewTarget }

public abstract class SkillHit : ScriptableObject
{
    protected const string FILE_NAME = "Skill Hit ";
    protected const string MENU_NAME = "ScriptableObject/Skill/Hit/";

    [field: SerializeField] public SkillHitType Type { get; protected set; }

    public abstract IEnumerator CoCheckHit(SkillProjectile projectile);

    protected void OnHit(SkillProjectile projectile, Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                // damage enemy
                projectile.Damage(collision.GetComponentInParent<EnemyPlayer>());
                break;

            case "Wall":
                // just despawn projectile
                PoolingManager.GetInstance().Despawn<SkillProjectile>(projectile);
                break;
        }
    }
}

