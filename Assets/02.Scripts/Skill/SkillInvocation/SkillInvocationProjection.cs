using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Projection", menuName = MENU_NAME + "Projection")]
public class SkillInvocationProjection : SkillInvocation
{
    private void OnValidate()
    {
        Type = SkillInvocationType.Projection;
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

                Vector3 position = Player.Instance.transform.position;
                Vector3 direction = target.transform.position - position;

                projectile.Init(skill);
                projectile.Init(position, Vector3.one * skill.Stat.Scale, direction);
                projectile.Init();

                // wait for interval
                yield return WaitForSecondsFactory.Get(skill.Stat.Interval);
            }
        }
    }
}
