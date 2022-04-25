using UnityEngine;

public enum SkillPropertyType { None, Fire, Ice, Wind, Poison, Explosion, Curse }

public abstract class SkillProperty : ScriptableObject
{
    public const string FILE_NAME = "Skill Property ";
    public const string MENU_NAME = "ScriptableObject/Skill/Property/";

    [field: SerializeField] public SkillPropertyType Type { get; protected set; }
    [field: SerializeField] public Color Color { get; protected set; }

    public abstract void OnHit(SkillProjectile projectile, EnemyPlayer enemy);
}
