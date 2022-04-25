using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Poison", menuName = MENU_NAME + "Poison")]
public class SkillPropertyPoison : SkillProperty
{
    [SerializeField] private CrowdControlInfo _ccInfo;

    private void OnValidate()
    {
        Type = SkillPropertyType.Poison;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // apply damage
        enemy.BlinkColor = Color;
        enemy.Hp -= projectile.Stat.Damage;

        if (enemy.Hp > 0)
        {
            // check constraint and percent
            if (enemy.Enemy.Constraint != Constraint.Poison && RandomExtension.CheckPercent(_ccInfo.Percent))
            {
                Poisoning(enemy);
            }
        }
    }

    private void Poisoning(EnemyPlayer enemy)
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
                enemy.StartCoroutine(CoPoisoning(cc, enemy));
            }
        }

        // add new coroutine and start it
        else
        {
            cc = new CrowdControl(_ccInfo);
            enemy.CrowdControlDic.Add(cc.Type, cc);
            enemy.StartCoroutine(CoPoisoning(cc, enemy));
        }
    }

    private IEnumerator CoPoisoning(CrowdControl cc, EnemyPlayer enemy)
    {
        // damage is increased by time
        float increasing = 20f;
        float elapsed = 0;

        while (cc.Duration > 0)
        {
            enemy.BlinkColor = Color;
            enemy.Hp -= (int)(_ccInfo.Values[cc.Level] + (elapsed * increasing));
            yield return WaitForSecondsFactory.Get(_ccInfo.Intervals[cc.Level]);

            cc.Duration -= _ccInfo.Intervals[cc.Level];
            elapsed += _ccInfo.Intervals[cc.Level];
        }
    }
}
