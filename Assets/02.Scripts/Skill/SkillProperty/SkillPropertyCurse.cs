using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Curse", menuName = MENU_NAME + "Curse")]
public class SkillPropertyCurse : SkillProperty
{
    [SerializeField] private EnemyMovementCursed _emc;
    [SerializeField] private CrowdControlInfo _ccInfo;

    private void OnValidate()
    {
        Type = SkillPropertyType.Curse;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // apply damage
        enemy.BlinkColor = Color;
        enemy.Hp -= projectile.Stat.Damage;

        if (enemy.Hp > 0)
        {
            // check constraint and percent
            if (enemy.Enemy.Constraint != Constraint.Curse && RandomExtension.CheckPercent(_ccInfo.Percent))
            {
                Cursed(enemy);
            }
        }
    }

    private void Cursed(EnemyPlayer enemy)
    {
        // if enemy has already crowd controlled, continue it
        if (enemy.CrowdControlDic.TryGetValue(_ccInfo.Type, out CrowdControl cc))
        {
            // if crowd control is active, just reset the duration
            if (cc.IsActive)
            {
                cc.Duration = _ccInfo.Durations[cc.Level];
            }

            // restart coroutine
            else
            {
                cc.Reset(_ccInfo);
                enemy.StartCoroutine(CoCursed(cc, enemy));
            }
        }

        // add new coroutine and start it
        else
        {
            cc = new CrowdControl(_ccInfo);
            enemy.CrowdControlDic.Add(cc.Type, cc);
            enemy.StartCoroutine(CoCursed(cc, enemy));
        }
    }

    private IEnumerator CoCursed(CrowdControl cc, EnemyPlayer enemy)
    {
        // set color
        enemy.Color = Color;
        // change enemy's movement type
        EnemyMovement org = enemy.Movement;
        enemy.Movement = _emc;

        yield return WaitForSecondsFactory.Get(cc.Duration);

        // recover color
        enemy.Color = default;
        // recover enemy's movement type
        enemy.Movement = org;
    }
}
