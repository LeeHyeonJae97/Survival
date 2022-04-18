using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillFactory
{
    public static int Count => Infos.Length;

    private static Skill[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Infos.OrderBy((x) => x.Id).ToArray();

                // load saved data
                JsonFileSystem<SkillData>.Load(SkillData.DIR_PATH, SkillData.FILE_PATH, out SkillData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < data.Skills.Length; i++) data.Skills[i].Init(infos[i]);

                // cache the data
                _list = data.Skills;
            }
            return _list;
        }
    }
    private static Dictionary<int, Skill> Dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, Skill>();

                for (int i = 0; i < List.Length; i++)
                {
                    _dic.Add(List[i].Info.Id, List[i]);
                }
            }
            return _dic;
        }
    }
    private static SkillSO[] Infos
    {
        get
        {
            if (_infos == null) _infos = Resources.LoadAll<SkillSO>("SkillSO");
            return _infos;
        }
    }

    private static Skill[] _list;
    private static Dictionary<int, Skill> _dic;
    private static SkillSO[] _infos;

    public static Skill Get(int id)
    {
        if (!Dic.TryGetValue(id, out Skill skill))
        {
            Debug.LogError($"There's no Skill : {id}");
        }
        return skill;
    }

    public static Skill[] GetAll()
    {
        return List;
    }

    public static Skill[] GetRandom(int count)
    {
        if (count < 1)
        {
            Debug.LogError("count should more than zero");
            return null;
        }

        // TODO :
        // upgrade to 'Fisher-Yates shuffle'

        count = Mathf.Min(count, List.Length);

        System.Random random = new System.Random();
        return List.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}

