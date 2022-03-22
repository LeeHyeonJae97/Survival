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
            GameObject target = skill.Targeting.GetTarget();

            // if there's no enemy just skip
            if (target != null)
            {
                // get projectile and initialize it
                SkillProjectile projectile = PoolingManager.Instance.Spawn<SkillProjectile>("SkillProjectile");
                projectile.Init(skill);

                // invoke skill with projectile and target
                Vector3 position = Player.Instance.transform.position;
                Vector3 direction = target.transform.position - position;
                projectile.Init(position, direction);

                // wait for interval
                yield return WaitForSecondsFactory.Get(skill.Stat.Interval);
            }
        }
    }
}
