using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "NewTarget", menuName = MENU_NAME + "NewTarget")]
public class SkillHitNewTarget : SkillHit
{
    private void OnValidate()
    {
        Type = SkillHitType.NewTarget;
    }

    public override void Hit_OnTriggerEnter2D(SkillProjectile projectile, Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                // set new target
                GameObject newTarget = projectile.Targeting.GetTarget();
                if (newTarget != null)
                {
                    Vector3 direction = newTarget.transform.position - projectile.transform.position;
                    projectile.Init(projectile.transform.position, direction);
                }
                break;

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
