using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Water", menuName = MENU_NAME + "Water")]
public class SkillPropertyWater : SkillProperty
{
    private void OnValidate()
    {
        Type = SkillPropertyType.Water;
    }

    public override void OnHit(Enemy enemy)
    {

    }
}
