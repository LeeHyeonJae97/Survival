using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Despawn", menuName = MENU_NAME + "Despawn")]
public class SkillOnHitDespawn : SkillOnHit
{
    private void OnValidate()
    {
        Type = SkillOnHitType.Despawn;
    }

    public override void OnHit(SkillProjectile projectile)
    {
        // despawn
        PoolingManager.GetInstance().Despawn<SkillProjectile>(projectile);
    }
}
