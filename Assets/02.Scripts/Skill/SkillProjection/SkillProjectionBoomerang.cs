using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Boomerang", menuName = MENU_NAME + "Boomerang")]
public class SkillProjectionBoomerang : SkillProjection
{
    private void OnValidate()
    {
        Type = SkillProjectionType.Boomerang;
    }

    public override void Projection_Start(SkillProjectile projectile)
    {

    }

    public override void Projection_Update(SkillProjectile projectile)
    {
        if (projectile.Elapsed > projectile.Stat.LifeSpan / 2)
        {
            // when projectile retrieved by player, just despawn it
            if ((projectile.transform.position - Player.Instance.transform.position).sqrMagnitude < 0.01f)
            {
                PoolingManager.Instance.Despawn<SkillProjectile>(projectile);
            }

            // set projectile's move direction
            projectile.transform.right = Player.Instance.transform.position - projectile.transform.position;
        }

        // move
        projectile.transform.Translate(Vector2.right * projectile.Stat.FlySpeed * PlayTime.deltaTime);
    }
}
