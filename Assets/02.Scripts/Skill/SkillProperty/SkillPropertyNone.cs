using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "None", menuName = MENU_NAME + "None")]
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
