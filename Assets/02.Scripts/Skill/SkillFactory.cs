using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillFactory
{
    public static int Count { get; private set; }

    private static Skill[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Resources.LoadAll<SkillSO>("SkillSO");
                infos = infos.OrderBy((x) => x.Id).ToArray();

                // save the count
                Count = infos.Length;

                // load saved data
                JsonFileSystem<SkillData>.Load(SkillData.DIR_PATH, SkillData.FILE_PATH, out SkillData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < _list.Length; i++) _list[i].Init(infos[i]);

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

    private static Skill[] _list;
    private static Dictionary<int, Skill> _dic;

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

        System.Random random = new System.Random();
        return List.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}

