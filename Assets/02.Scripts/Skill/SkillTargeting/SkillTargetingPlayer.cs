using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Player", menuName = MENU_NAME + "Player")]
public class SkillTargetingPlayer : SkillTargeting
{
    private void OnValidate()
    {
        Type = SkillTargetingType.Player;
    }

    public override GameObject GetTarget(Vector3 position, float range)
    {
        return Player.Instance.gameObject;
    }
}
