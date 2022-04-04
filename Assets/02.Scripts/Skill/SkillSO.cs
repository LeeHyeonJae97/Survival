using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill ", menuName = "ScriptableObject/Skill/Skill")]
public class SkillSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GradeType Grade { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite ProjectileSprite { get; private set; }
    [field: SerializeField] public SkillStat[] Stats { get; private set; }
    [field: SerializeField] public SkillProperty Property { get; private set; }
    [field: SerializeField] public SkillInvocation Invocation { get; private set; }
    [field: SerializeField] public SkillTargeting Targeting { get; private set; }
    [field: SerializeField] public SkillProjection Projection { get; private set; }
    [field: SerializeField] public SkillHit Hit { get; private set; }
    [field: SerializeField] public SkillOnHit OnHit { get; private set; }
}