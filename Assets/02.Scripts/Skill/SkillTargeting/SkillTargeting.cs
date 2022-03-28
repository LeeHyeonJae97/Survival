using UnityEngine;

public enum SkillTargetingType { Closest, Random, Player }

public abstract class SkillTargeting : ScriptableObject
{
    public const string FILE_NAME = "Skill Targeting ";
    public const string MENU_NAME = "ScriptableObject/Skill/Targeting/";

    [field: SerializeField] public SkillTargetingType Type { get; protected set; }

    public abstract GameObject GetTarget(Vector3 position, float range);
}
