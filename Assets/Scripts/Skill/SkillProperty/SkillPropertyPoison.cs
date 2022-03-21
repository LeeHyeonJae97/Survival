using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Poison", menuName = MENU_NAME + "Poison")]
public class SkillPropertyPoison : SkillProperty
{
    private void OnValidate()
    {
        Type = SkillPropertyType.Poison;
    }

    public override void OnHit(Enemy enemy)
    {

    }
}
