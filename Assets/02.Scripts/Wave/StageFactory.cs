using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageFactory
{
    public static int Count => Infos.Length;

    private static Stage[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Infos.OrderBy((x) => x.Id).ToArray();

                // load saved data
                JsonFileSystem<StageData>.Load(StageData.DIR_PATH, StageData.FILE_PATH, out StageData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < data.Stages.Length; i++) data.Stages[i].Init(infos[i]);

                // cache the data
                _list = data.Stages;
            }
            return _list;
        }
    }
    private static Dictionary<int, Stage> Dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, Stage>();

                for (int i = 0; i < List.Length; i++)
                {
                    _dic.Add(List[i].Info.Id, List[i]);
                }
            }
            return _dic;
        }
    }
    private static StageSO[] Infos
    {
        get
        {
            if (_infos == null) _infos = Resources.LoadAll<StageSO>("StageSO");
            return _infos;
        }
    }

    private static Stage[] _list;
    private static Dictionary<int, Stage> _dic;
    private static StageSO[] _infos;

    public static Stage Get(int id)
    {
        if (!Dic.TryGetValue(id, out Stage skill))
        {
            Debug.LogError($"There's no Stage : {id}");
        }
        return skill;
    }

    public static Stage[] GetAll()
    {
        return List;
    }
}
