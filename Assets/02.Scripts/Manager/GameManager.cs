using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public UserData User
    {
        get
        {
            if (_user == null) JsonFileSystem<UserData>.Load(UserData.DIR_PATH, UserData.FILE_PATH, out _user);
            return _user;
        }
    }

    private UserData _user;

    private void OnDisable()
    {
        JsonFileSystem<UserData>.Save(UserData.DIR_PATH, UserData.FILE_PATH, _user);
        JsonFileSystem<SkillData>.Save(SkillData.DIR_PATH, SkillData.FILE_PATH, new SkillData(SkillFactory.GetAll()));
    }
}
