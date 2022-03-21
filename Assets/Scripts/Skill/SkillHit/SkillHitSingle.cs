using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Single", menuName = MENU_NAME + "Single")]
public class SkillHitSingle : SkillHit
{
    private void OnValidate()
    {
        Type = SkillHitType.Single;
    }

    public override void Hit_OnTriggerEnter2D(SkillProjectile projectile, Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                // damage enemy and despawn it
                projectile.Damage(collision.GetComponentInParent<Enemy>());
                PoolingManager.Instance.Despawn<SkillProjectile>(projectile);
                break;

            case "Wall":
                // just despawn projectile
                PoolingManager.Instance.Despawn<SkillProjectile>(projectile);
                break;
        }
    }

    public override void Hit_OnTriggerStay2D(SkillProjectile projectile, Collider2D collision)
    {

    }
}
