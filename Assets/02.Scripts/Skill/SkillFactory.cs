using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static SkillSO[] GetRandom(int count)
    {
        if (count < 1)
        {
            Debug.LogError("count should more than zero");
            return null;
        }

        // TODO :
        // upgrade to 'Fisher-Yates shuffle'

        System.Random random = new System.Random();
        return _dic.Values.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}
