using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Multiple", menuName = MENU_NAME + "Multiple")]
public class SkillHitMultiple : SkillHit
{
    [SerializeField] private int _maxHitCount;

    private void OnValidate()
    {
        Type = SkillHitType.Multiple;
    }

    public override IEnumerator CoCheckHit(SkillProjectile projectile)
    {
        while (true)
        {
            Collider2D[] colls = Physics2D.OverlapCircleAll(projectile.transform.position,
                  projectile.Stat.Scale / 2, 1 << LayerMask.NameToLayer("Enemy"));

            if (colls != null)
            {
                for (int i = 0; i < colls.Length && i< _maxHitCount; i++)
                {
                    OnHit(projectile, colls[i]);
                }
                projectile.OnHit.OnHit(projectile);
            }
            yield return WaitForSecondsFactory.GetPlayTime(projectile.Stat.DamagingCooldown);
        }
    }
}
