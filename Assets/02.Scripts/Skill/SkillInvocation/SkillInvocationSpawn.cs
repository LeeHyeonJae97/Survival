using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Spawn", menuName = MENU_NAME + "Spawn")]
public class SkillInvocationSpawn : SkillInvocation
{
    private void OnValidate()
    {
        Type = SkillInvocationType.Spawn;
    }

    public override IEnumerator CoInvoke(Skill skill)
    {
        for (int i = 0; i < skill.Stat.Amount; i++)
        {
            // get target
            GameObject target = skill.Targeting.GetTarget();

            // if there's no enemy just skip
            if (target != null)
            {
                // get projectile and initialize it
                var projectile = PoolingManager.Instance.Spawn<SkillProjectile>();
                projectile.Init(skill);

                // invoke skill with projectile and target
                projectile.Init(target.transform.position);

                // wait for interval
                yield return WaitForSecondsFactory.Get(skill.Stat.Interval);
            }
        }
    }
}
