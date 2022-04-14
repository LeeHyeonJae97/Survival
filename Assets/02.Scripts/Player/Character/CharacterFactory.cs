using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterFactory
{
    public static int Count => Infos.Length;

    private static Character[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Infos.OrderBy((x) => x.Id).ToArray();

                // load saved data
                JsonFileSystem<CharacterData>.Load(CharacterData.DIR_PATH, CharacterData.FILE_PATH, out CharacterData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < data.Characters.Length; i++) data.Characters[i].Init(infos[i]);

                // cache the data
                _list = data.Characters;
            }
            return _list;
        }
    }
    private static Dictionary<int, Character> Dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, Character>();

                for (int i = 0; i < List.Length; i++)
                {
                    _dic.Add(List[i].Info.Id, List[i]);
                }
            }
            return _dic;
        }
    }
    private static CharacterSO[] Infos
    {
        get
        {
            if (_infos == null) _infos = Resources.LoadAll<CharacterSO>("CharacterSO");
            return _infos;
        }
    }

    private static Character[] _list;
    private static Dictionary<int, Character> _dic;
    private static CharacterSO[] _infos;

    public static Character Get(int id)
    {
        if (!Dic.TryGetValue(id, out Character character))
        {
            Debug.LogError($"There's no Character : {id}");
        }
        return character;
    }

    public static Character[] GetAll()
    {
        return List;
    }
}
