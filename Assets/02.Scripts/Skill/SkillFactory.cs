using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    public static int Count => Dic.Count;

    private static Dictionary<int, SkillSO> Dic
    {
        get
        {
            if (_dic == null)
            {
                var skills = Resources.LoadAll<SkillSO>("Skill");

                _dic = new Dictionary<int, SkillSO>();
                for (int i = 0; i < skills.Length; i++)
                {
                    _dic.Add(skills[i].Id, skills[i]);
                }
            }
            return _dic;
        }
    }
    private static Dictionary<int, SkillSO> _dic;

    public static SkillSO Get(int id)
    {
        if (!Dic.TryGetValue(id, out SkillSO skill))
        {
            Debug.LogError($"There's no Skill : {id}");
        }
        return skill;
    }
}
