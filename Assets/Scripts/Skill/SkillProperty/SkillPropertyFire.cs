using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Fire", menuName = MENU_NAME + "Fire")]
public class SkillPropertyFire : SkillProperty
{
    private void OnValidate()
    {
        Type = SkillPropertyType.Fire;
    }

    public override void OnHit(Enemy enemy)
    {

    }
}
