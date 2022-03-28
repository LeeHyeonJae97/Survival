using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPropertyNone : SkillProperty
{
    private void OnValidate()
    {
        Type = SkillPropertyType.None;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {

    }
}
