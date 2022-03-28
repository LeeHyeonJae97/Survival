using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "FindNewTarget", menuName = MENU_NAME + "FindNewTarget")]
public class SkillOnHitFindNewTarget : SkillOnHit
{
    [SerializeField] private float _newTargetingRange;

    private void OnValidate()
    {
        Type = SkillOnHitType.FindNewTarget;
    }

    public override void OnHit(SkillProjectile projectile)
    {
        // set new target
        GameObject newTarget = projectile.Targeting.GetTarget(projectile.transform.position, _newTargetingRange);

        if (newTarget != null)
        {
            Vector3 direction = newTarget.transform.position - projectile.transform.position;
            projectile.Init(direction: direction);
        }
    }
}
