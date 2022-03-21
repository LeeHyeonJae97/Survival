using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Electricity", menuName = MENU_NAME + "Electricity")]
public class SkillPropertyElectricity : SkillProperty
{
    private void OnValidate()
    {
        Type = SkillPropertyType.Electricity;
    }

    public override void OnHit(Enemy enemy)
    {

    }
}
