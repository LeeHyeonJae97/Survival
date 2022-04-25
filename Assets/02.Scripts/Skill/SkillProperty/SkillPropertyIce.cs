using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Ice", menuName = MENU_NAME + "Ice")]
public class SkillPropertyIce : SkillProperty
{
    [SerializeField] private CrowdControlInfo _ccInfo;

    private void OnValidate()
    {
        Type = SkillPropertyType.Ice;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // apply damage
        enemy.BlinkColor = Color;
        enemy.Hp -= projectile.Stat.Damage;

        if (enemy.Hp > 0)
        {
            // check constraint and percent
            if (enemy.Enemy.Constraint != Constraint.Ice && RandomExtension.CheckPercent(_ccInfo.Percent))
            {
                SlowDown(enemy);
            }
        }
    }
    

    private void SlowDown(EnemyPlayer enemy)
    {
        // if enemy has already crowd controlled before, continue it
        if (enemy.CrowdControlDic.TryGetValue(_ccInfo.Type, out CrowdControl cc))
        {
            // if crowd control is active, just reset the duration
            if (cc.IsActive)
            {
                // if more than half of the duration is remained, level up the cc
                if (cc.Duration > _ccInfo.Durations[cc.Level] / 2)
                {
                    cc.Level = Mathf.Min(cc.Level + 1, CrowdControlInfo.MAX_LEVEL);
                }
                cc.Duration = _ccInfo.Durations[cc.Level];
            }

            // restart coroutine
            else
            {
                cc.Reset(_ccInfo);
                enemy.StartCoroutine(CoSlowDown(cc, enemy));
            }
        }

        // add new coroutine and start it
        else
        {
            cc = new CrowdControl(_ccInfo);
            enemy.CrowdControlDic.Add(cc.Type, cc);
            enemy.StartCoroutine(CoSlowDown(cc, enemy));
        }
    }

    private IEnumerator CoSlowDown(CrowdControl cc, EnemyPlayer enemy)
    {
        // set color
        enemy.Color = Color;

        while (cc.Duration > 0)
        {
            // TODO :
            // need optimization
            // cost of calling getter and setter per frame can be very expensive
            // and also there can be lots of active enemies
            enemy.Speed = enemy.Enemy.Stats[(int)StatType.Speed].Value - _ccInfo.Values[cc.Level];
            yield return null;

            cc.Duration -= PlayTime.deltaTime;
        }

        // recover color
        enemy.Color = default;
        // recover speed
        enemy.Speed = enemy.Enemy.Stats[(int)StatType.Speed].Value;
    }
}
