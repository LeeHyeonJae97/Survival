using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "None", menuName = MENU_NAME + "None")]
public class SkillOnHitNone : SkillOnHit
{
    private void OnValidate()
    {
        Type = SkillOnHitType.None;
    }

    public override void OnHit(SkillProjectile projectile)
    {
        // do nothing
    }
}
