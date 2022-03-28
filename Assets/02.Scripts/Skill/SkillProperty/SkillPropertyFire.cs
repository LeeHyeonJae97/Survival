using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Fire", menuName = MENU_NAME + "Fire")]
public class SkillPropertyFire : SkillProperty
{
    [SerializeField] private CrowdControlInfo _ccInfo;

    private void OnValidate()
    {
        Type = SkillPropertyType.Fire;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // check constraint
        if (enemy.Enemy.Constraint == Constraint.Fire) return;

        // check percent
        if (!RandomExtension.CheckPercent(_ccInfo.Percent)) return;

        Burning(enemy);
    }

    private void Burning(EnemyPlayer enemy)
    {
        // if enemy has already crowd controlled before, continue it
        if (enemy.CrowdControlDic.TryGetValue(_ccInfo.Type, out CrowdControl cc))
        {
            if (cc.Infinite) return;

            // if crowd control is active, just reset the duration
            if (cc.IsActive)
            {
                if (cc.Duration > _ccInfo.Durations[cc.Level] / 2)
                {
                    cc.Level = Mathf.Min(cc.Level + 1, CrowdControlInfo.MAX_LEVEL);
                }

                if (cc.Level == CrowdControlInfo.MAX_LEVEL)
                {
                    cc.Infinite = true;
                }
                else
                {
                    cc.Duration = _ccInfo.Durations[cc.Level];
                }
            }
            // restart coroutine
            else
            {
                cc.Reset(_ccInfo);
                enemy.StartCoroutine(CoBurning(cc, enemy));
            }
        }

        // add new coroutine and start it
        else
        {
            cc = new CrowdControl(_ccInfo);
            enemy.CrowdControlDic.Add(cc.Type, cc);
            enemy.StartCoroutine(CoBurning(cc, enemy));
        }
    }

    private IEnumerator CoBurning(CrowdControl cc, EnemyPlayer enemy)
    {
        // spawn icon
        var icon = PoolingManager.Instance.Spawn<SpriteRenderer>("CrowdControlIcon");
        icon.sprite = _ccInfo.Icon;
        icon.transform.SetParent(enemy.CrowdControlIconsHolder);

        while (cc.Infinite || cc.Duration > 0)
        {
            enemy.HP -= (int)_ccInfo.Values[cc.Level];
            yield return WaitForSecondsFactory.Get(_ccInfo.Intervals[cc.Level]);

            if (!cc.Infinite) cc.Duration -= _ccInfo.Intervals[cc.Level];
        }

        // despawn icon
        PoolingManager.Instance.Despawn<SpriteRenderer>(icon);
    }
}