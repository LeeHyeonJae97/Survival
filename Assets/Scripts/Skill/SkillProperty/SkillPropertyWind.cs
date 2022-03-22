using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Wind", menuName = MENU_NAME + "Wind")]
public class SkillPropertyWind : SkillProperty
{
    [SerializeField] private int _percent;
    [SerializeField] private float _knockback;

    private void OnValidate()
    {
        Type = SkillPropertyType.Wind;
    }

    public override void OnHit(SkillProjectile projectile, Enemy enemy)
    {
        // check percent
        if (!RandomExtension.CheckPercent(_percent)) return;

        // knockback enemy
        enemy.transform.position += (enemy.transform.position - projectile.transform.position).normalized * _knockback;
    }
}
