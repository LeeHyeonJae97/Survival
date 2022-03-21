using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Straight", menuName = MENU_NAME + "Straight")]
public class SkillProjectionStraight : SkillProjection
{
    private void OnValidate()
    {
        Type = SkillProjectionType.Straight;
    }

    public override void Projection_Start(SkillProjectile projectile)
    {

    }

    public override void Projection_Update(SkillProjectile projectile)
    {
        projectile.transform.Translate(Vector2.right * projectile.Stat.FlySpeed * Time.deltaTime);
    }
}
