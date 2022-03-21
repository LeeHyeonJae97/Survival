using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Closest", menuName = MENU_NAME + "Closest")]
public class SkillTargetingClosest : SkillTargeting
{
    private void OnValidate()
    {
        Type = SkillTargetingType.Closest;
    }

    public override Enemy GetTarget()
    {
        return EnemySpawner.Instance.Closest(Player.Instance.transform.position);
    }
}
