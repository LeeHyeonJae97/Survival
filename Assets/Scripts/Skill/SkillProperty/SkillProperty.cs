using UnityEngine;

public enum SkillPropertyType { Fire, Ice, Wind, Poison, Explosion }

public abstract class SkillProperty : ScriptableObject
{
    public const string FILE_NAME = "Skill Property ";
    public const string MENU_NAME = "ScriptableObject/Skill/Property/";

    [field: SerializeField] public SkillPropertyType Type { get; protected set; }

    public abstract void OnHit(SkillProjectile projectile, Enemy enemy);
}
