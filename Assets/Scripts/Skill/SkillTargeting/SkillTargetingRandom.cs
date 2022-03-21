using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Random", menuName = MENU_NAME + "Random")]
public class SkillTargetingRandom : SkillTargeting
{
    private void OnValidate()
    {
        Type = SkillTargetingType.Random;
    }

    public override Enemy GetTarget()
    {
        return EnemySpawner.Instance.Random();
    }
}
