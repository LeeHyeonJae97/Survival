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

                projectile.Init(liveSkill);
                projectile.Init(target.transform.position, Vector3.one * stat.Scale, parent: target.transform);
                projectile.Init();

                // wait for interval
                yield return WaitForSecondsFactory.GetPlayTime(stat.Interval);
            }
        }
    }
}
