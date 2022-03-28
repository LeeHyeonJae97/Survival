using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "PingPong", menuName = MENU_NAME + "PingPong")]
public class SkillProjectionPingPong : SkillProjection
{
    [SerializeField] private int _count;

    private void OnValidate()
    {
        Type = SkillProjectionType.PingPong;
    }

    public override void Projection_Start(SkillProjectile projectile)
    {
        float duration = projectile.Stat.LifeSpan / (_count * 2);
        Vector2 target = projectile.transform.position + projectile.transform.right * projectile.Stat.FlySpeed * duration;

        projectile.transform.DOMove(target, duration).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Projection_Update(SkillProjectile projectile)
    {

    }
}
