using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Child", menuName = MENU_NAME + "Child")]
public class SkillInvocationChild : SkillInvocation
{
    private void OnValidate()
    {
        Type = SkillInvocationType.Child;
    }

    public override IEnumerator CoInvoke(Skill skill)
    {
        for (int i = 0; i < skill.Stat.Amount; i++)
        {
            // get target
            GameObject target = skill.Targeting.GetTarget(Player.Instance.transform.position, skill.Stat.TargetingRange);

            // if there's no enemy just skip
            if (target != null)
            {
                // get projectile and initialize it
                var projectile = PoolingManager.Instance.Spawn<SkillProjectile>();

                projectile.Init(skill);
                projectile.Init(target.transform.position, Vector3.one * skill.Stat.Scale, parent: target.transform);
                projectile.Init();

                // wait for interval
                yield return WaitForSecondsFactory.Get(skill.Stat.Interval);
            }
        }
    }
}
