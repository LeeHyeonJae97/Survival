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

    public override IEnumerator CoInvoke(LiveSkill liveSkill)
    {
        SkillStatSO stat = liveSkill.Skill.Info.Stats[liveSkill.Level].Stats[liveSkill.Skill.Reinforced];

        for (int i = 0; i < stat.Amount; i++)
        {
            // get target
            GameObject target = liveSkill.Skill.Info.Targeting.GetTarget(Player.Instance.transform.position, stat.TargetingRange);

            // if there's no enemy just skip
            if (target != null)
            {
                // get projectile and initialize it
                var projectile = PoolingManager.Instance.Spawn<SkillProjectile>();

                projectile.Init(liveSkill);
                projectile.Init(target.transform.position);
                projectile.Init();

                // wait for interval
                yield return WaitForSecondsFactory.Get(stat.Interval);
            }
        }
    }
}
