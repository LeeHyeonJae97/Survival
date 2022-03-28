using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Multiple", menuName = MENU_NAME + "Multiple")]
public class SkillHitMultiple : SkillHit
{
    private void OnValidate()
    {
        Type = SkillHitType.Multiple;
    }

    public override void Hit_OnTriggerEnter2D(SkillProjectile projectile, Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Wall":
                // just despawn projectile
                PoolingManager.Instance.Despawn<SkillProjectile>(projectile);
                break;
        }
    }

    public override void Hit_OnTriggerStay2D(SkillProjectile projectile, Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                // damage enemy
                projectile.Damage(collision.GetComponentInParent<EnemyPlayer>());
                break;
        }
    }
}
