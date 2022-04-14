using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillStat", menuName = "ScriptableObject/Skill/SkillStat")]
public class SkillStatSO : ScriptableObject
{
    [field: SerializeField] public SkillStat[] Stats { get; private set; }

    public void OnValidate()
    {
        if (Stats == null)
        {
            Stats = new SkillStat[Skill.MAX_REINFORCED];
        }
        else if (Stats.Length != Skill.MAX_REINFORCED)
        {
            SkillStat[] replace = new SkillStat[Skill.MAX_REINFORCED];

            for (int i = 0; i < Stats.Length && i < Skill.MAX_REINFORCED; i++)
            {
                replace[i] = Stats[i];
            }
            Stats = replace;
        }
    }
}
