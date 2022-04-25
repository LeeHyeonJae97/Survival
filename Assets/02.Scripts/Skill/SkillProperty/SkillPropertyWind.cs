using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Wind", menuName = MENU_NAME + "Wind")]
public class SkillPropertyWind : SkillProperty
{
    [SerializeField] private CrowdControlInfo _ccInfo;

    private void OnValidate()
    {
        Type = SkillPropertyType.Wind;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // apply damage
        enemy.BlinkColor = Color;
        enemy.Hp -= projectile.Stat.Damage;

        if (enemy.Hp > 0)
        {
            // check constraint and percent
            if (enemy.Enemy.Constraint != Constraint.Wind && RandomExtension.CheckPercent(_ccInfo.Percent))
            {
                // knockback enemy
                enemy.transform.position += (enemy.transform.position - projectile.transform.position).normalized * _ccInfo.Values[0];
            }
        }
    }
}
