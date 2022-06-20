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

    public override IEnumerator CoInvoke(LiveSkill liveSkill)
    {
        SkillStat stat = liveSkill.Skill.Info.Stats[liveSkill.Level].Stats[liveSkill.Skill.Reinforced];

        for (int i = 0; i < stat.Amount; i++)
        {
            // get target
            GameObject target = liveSkill.Skill.Info.Targeting.GetTarget(Player.GetInstance().transform.position, stat.TargetingRange);

            // if there's no enemy just skip
            if (target != null)
            {
                // get projectile and initialize it
                var projectile = PoolingManager.GetInstance().Spawn<SkillProjectile>();

                Vector3 position = Player.GetInstance().transform.position;
                Vector3 direction = target.transform.position - position;

                projectile.Init(liveSkill);
                projectile.Init(position, Vector3.one * stat.Scale, direction);
                projectile.Init();

                // wait for interval
                yield return WaitForSecondsFactory.GetPlayTime(stat.Interval);
            }
        }
    }
}
