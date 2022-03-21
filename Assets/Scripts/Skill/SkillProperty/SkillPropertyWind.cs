using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Wind", menuName = MENU_NAME + "Wind")]
public class SkillPropertyWind : SkillProperty
{
    private void OnValidate()
    {
        Type = SkillPropertyType.Wind;
    }

    public override void OnHit(Enemy enemy)
    {

    }
}
