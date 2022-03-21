using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Sin", menuName = MENU_NAME + "Sin")]
public class SkillProjectionSin : SkillProjection
{
    [SerializeField] private float _rotateSpeed = 3f;

    private void OnValidate()
    {
        Type = SkillProjectionType.Sin;
    }

    public override void Projection_Start(SkillProjectile projectile)
    {

    }

    public override void Projection_Update(SkillProjectile projectile)
    {
        // TODO :

        //projectile.transform.right += projectile.transform.TransformDirection(
        //    new Vector2(0, (Mathf.PingPong(Time.timeSinceLevelLoad, 1) * 2 - 1) * _rotateSpeed * Time.deltaTime));
        //projectile.transform.Translate(Vector2.right * projectile.Stat.FlySpeed * Time.deltaTime);
    }
}
