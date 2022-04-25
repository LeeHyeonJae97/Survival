using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Spiral", menuName = MENU_NAME + "Spiral")]
public class SkillProjectionSpiral : SkillProjection
{
    [SerializeField] private float _rotateSpeed = 30f;

    private void OnValidate()
    {
        Type = SkillProjectionType.Spiral;
    }

    public override void Projection_Start(SkillProjectile projectile)
    {

    }

    public override void Projection_Update(SkillProjectile projectile)
    {
        projectile.transform.Rotate(Vector3.forward, Mathf.PingPong(Time.timeSinceLevelLoad, 1) * _rotateSpeed * PlayTime.deltaTime);
        projectile.transform.Translate(Vector2.right * projectile.Stat.FlySpeed * PlayTime.deltaTime);
    }
}
