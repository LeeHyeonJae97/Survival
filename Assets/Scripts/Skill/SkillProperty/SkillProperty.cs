using UnityEngine;

public enum SkillPropertyType { Fire, Water, Wind, Poison, Electricity }

public abstract class SkillProperty : ScriptableObject
{
    public const string FILE_NAME = "Skill Property ";
    public const string MENU_NAME = "ScriptableObject/Skill/Property/";

    [field: SerializeField] public SkillPropertyType Type { get; protected set; }

    public abstract void OnHit(Enemy enemy);
}
