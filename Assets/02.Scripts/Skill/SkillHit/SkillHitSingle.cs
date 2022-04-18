using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Single", menuName = MENU_NAME + "Single")]
public class SkillHitSingle : SkillHit
{
    private void OnValidate()
    {
        Type = SkillHitType.Single;
    }

    public override IEnumerator CoCheckHit(SkillProjectile projectile)
    {
        while (true)
        {
            Collider2D coll = Physics2D.OverlapCircle(projectile.transform.position,
                projectile.Stat.Scale / 2, 1 << LayerMask.NameToLayer("Enemy"));

            if (coll != null)
            {
                OnHit(projectile, coll);
                projectile.OnHit.OnHit(projectile);

            }

            // if despawned on hit enemy, just stop coroutine
            if (!projectile.isActiveAndEnabled) yield break;

            yield return WaitForSecondsFactory.Get(projectile.Stat.DamagingCooldown);
        }
    }
}
