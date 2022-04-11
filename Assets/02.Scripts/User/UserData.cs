using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public const string DIR_PATH = "UserData";
    public const string FILE_PATH = "userData.json";

    [field: SerializeField] public int Coin { get; private set; }
    [field: SerializeField] public bool Mute { get; set; }
    [field: SerializeField] public bool Vibration { get; set; }
    
    public UserData()
    {
        Coin = 0;
        Mute = false;
        Vibration = true;
    }

    public void Load()
    {
        // if there's no file, make new file
        if (!Directory.Exists(DIR_PATH) || !File.Exists(FILE_PATH))
        {
            Coin = 0;
            Mute = false;
            Vibration = true;

            Save();
        }

        // if not, read file and load data
        else
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(FILE_PATH), this);
        }
    }

    public void Save()
    {
        // if there's no directory create it newly
        if (!Directory.Exists(DIR_PATH)) Directory.CreateDirectory(DIR_PATH);

        // save file in the directory
        File.WriteAllText(FILE_PATH, JsonUtility.ToJson(this));
    }
}
