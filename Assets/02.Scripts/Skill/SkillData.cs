using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public const string DIR_PATH = "SkillData";
    public const string FILE_PATH = "skillData.json";

    [field: SerializeField] public Skill[] Skills { get; private set; }

    public SkillData()
    {
        Skills = new Skill[SkillFactory.Count];
        for (int i = 0; i < Skills.Length; i++) Skills[i] = new Skill();
    }

    public SkillData(Skill[] skills)
    {
        this.Skills = skills;
    }
}
