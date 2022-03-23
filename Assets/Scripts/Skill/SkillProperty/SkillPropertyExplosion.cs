using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Explosion", menuName = MENU_NAME + "Explosion")]
public class SkillPropertyExplosion : SkillProperty
{
    [SerializeField] private int _percent;
    [SerializeField] private float _splash;
    [SerializeField] private float _range;

    private void OnValidate()
    {
        Type = SkillPropertyType.Explosion;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // check percent
        if (!RandomExtension.CheckPercent(_percent)) return;

        // NOTICE :
        // enemy(from parameter) should be applied splash damage?
        // how to calculate splash damage? is it same with projectile's damage? or set constant?

        // get nearby enemies and apply splash damage
        Collider2D[] colls = Physics2D.OverlapCircleAll(projectile.transform.position, _range, 1 << LayerMask.NameToLayer("Enemy"));

        for (int i = 0; i < colls.Length; i++)
        {
            EnemyPlayer nearby = colls[i].GetComponentInParent<EnemyPlayer>();
            if (nearby != null)
            {
                nearby.HP -= (int)_splash;
            }
        }
    }
}
