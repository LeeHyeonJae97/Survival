using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "User", menuName = "ScriptableObject/User/User")]
public class UserSO : ScriptableObject
{
    public const string DIRECTORY = "Settings";
    public const string FILE = "settings.json";

    public string PATH_DIR => $"{Application.persistentDataPath}/{DIRECTORY}";
    public string PATH_FILE => $"{PATH_DIR}/{FILE}";

    [field: SerializeField] public IllustratedBook IllustratedBook { get; private set; }
    [field: SerializeField] public bool Mute { get; set; }
    [field: SerializeField] public bool Vibration { get; set; }

    public void Load()
    {
        // if there's no file, make new file
        if (!Directory.Exists(PATH_DIR) || !File.Exists(PATH_FILE))
        {
            IllustratedBook = new IllustratedBook(ItemFactory.Count, SkillFactory.Count, PotionFactory.Count, EnemyFactory.Count);
            Mute = false;

            Save();
        }

        // if not, read file and load data
        else
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(PATH_FILE), this);
        }

        //IllustratedBook = new IllustratedBook(ItemFactory.Count, SkillFactory.Count, PotionFactory.Count, EnemyFactory.Count);
        //Mute = false;
    }

    public void Save()
    {
        // if there's no directory create it newly
        if (!Directory.Exists(PATH_DIR)) Directory.CreateDirectory(PATH_DIR);

        // save file in the directory
        File.WriteAllText(PATH_FILE, JsonUtility.ToJson(this));
    }
}
