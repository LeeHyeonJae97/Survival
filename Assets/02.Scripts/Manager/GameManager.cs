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
        JsonFileSystem<CharacterData>.Save(CharacterData.DIR_PATH, CharacterData.FILE_PATH, new CharacterData(CharacterFactory.GetAll()));
        JsonFileSystem<SkillData>.Save(SkillData.DIR_PATH, SkillData.FILE_PATH, new SkillData(SkillFactory.GetAll()));
        JsonFileSystem<ItemData>.Save(ItemData.DIR_PATH, ItemData.FILE_PATH, new ItemData(ItemFactory.GetAll()));
        JsonFileSystem<PotionData>.Save(PotionData.DIR_PATH, PotionData.FILE_PATH, new PotionData(PotionFactory.GetAll()));
        JsonFileSystem<EnemyData>.Save(EnemyData.DIR_PATH, EnemyData.FILE_PATH, new EnemyData(EnemyFactory.GetAll()));
    }
}
