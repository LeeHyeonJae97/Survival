using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill ", menuName = "ScriptableObject/Skill/Skill")]
public class SkillSO : ScriptableObject
{
    public const int MAX_LEVEL = 3;

    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public GradeType Grade { get; private set; }
    [field: SerializeField] public string[] Names { get; private set; }
    [field: SerializeField] public string[] Descriptions { get; private set; }
    [field: SerializeField] public int[] Prices { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public Sprite ProjectileSprite { get; private set; }
    [field: SerializeField] public SkillStatSO[] Stats { get; private set; }
    [field: SerializeField] public SkillProperty Property { get; private set; }
    [field: SerializeField] public SkillInvocation Invocation { get; private set; }
    [field: SerializeField] public SkillTargeting Targeting { get; private set; }
    [field: SerializeField] public SkillProjection Projection { get; private set; }
    [field: SerializeField] public SkillHit Hit { get; private set; }
    [field: SerializeField] public SkillOnHit OnHit { get; private set; }

    //private void OnValidate()
    //{
    //    if (Names == null)
    //    {
    //        Names = new string[MAX_LEVEL];
    //    }
    //    else if (Names.Length != MAX_LEVEL)
    //    {
    //        string[] replace = new string[MAX_LEVEL];

    //        for (int i = 0; i < Names.Length && i < MAX_LEVEL; i++)
    //        {
    //            replace[i] = Names[i];
    //        }
    //        Names = replace;
    //    }

    //    if (Descriptions == null)
    //    {
    //        Descriptions = new string[MAX_LEVEL];
    //    }
    //    else if (Descriptions.Length != MAX_LEVEL)
    //    {
    //        string[] replace = new string[MAX_LEVEL];

    //        for (int i = 0; i < Descriptions.Length && i < MAX_LEVEL; i++)
    //        {
    //            replace[i] = Descriptions[i];
    //        }
    //        Descriptions = replace;
    //    }

    //    if (Stats == null)
    //    {
    //        Stats = new SkillStatSO[MAX_LEVEL];
    //    }
    //    else if (Stats.Length != MAX_LEVEL)
    //    {
    //        SkillStatSO[] replace = new SkillStatSO[MAX_LEVEL];

    //        for (int i = 0; i < Stats.Length && i < MAX_LEVEL; i++)
    //        {
    //            replace[i] = i < Stats.Length ? Stats[i] : null;
    //        }
    //        Stats = replace;
    //    }
    //}
}