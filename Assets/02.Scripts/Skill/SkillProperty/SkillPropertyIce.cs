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
        // check constraint
        if (enemy.Enemy.Constraint == Constraint.Ice) return;

        // check percent
        if (!RandomExtension.CheckPercent(_ccInfo.Percent)) return;

        SlowDown(enemy);
    }

    // CONTINUE :
    // need debugging
    private void SlowDown(EnemyPlayer enemy)
    {
        // if enemy has already crowd controlled before, continue it
        if (enemy.CrowdControlDic.TryGetValue(_ccInfo.Type, out CrowdControl cc))
        {
            // if crowd control is active, just reset the duration
            if (cc.IsActive)
            {
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
        // spawn icon
        var icon = PoolingManager.Instance.Spawn<SpriteRenderer>("CrowdControlIcon");
        icon.sprite = _ccInfo.Icon;
        icon.transform.SetParent(enemy.CrowdControlIconsHolder);

        while (cc.Duration > 0)
        {
            // TODO :
            // need optimization
            // cost of calling getter and setter per frame can be very expensive
            // and also there can be lots of active enemies
            enemy.Speed = enemy.Enemy.Speed - _ccInfo.Values[cc.Level];
            yield return null;

            cc.Duration -= Time.deltaTime;
        }

        // recover speed
        enemy.Speed = enemy.Enemy.Speed;
        
        // despawn icon
        PoolingManager.Instance.Despawn<SpriteRenderer>(icon);
    }
}
