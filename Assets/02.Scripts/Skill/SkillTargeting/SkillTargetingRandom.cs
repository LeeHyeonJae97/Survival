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

    public override GameObject GetTarget(Vector3 position, float range)
    {
        return WaveManager.GetInstance().Random(position, range)?.gameObject;
    }
}
