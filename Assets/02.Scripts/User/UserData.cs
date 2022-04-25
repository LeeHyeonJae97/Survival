using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public const string DIR_PATH = "UserData";
    public const string FILE_PATH = "userData.json";

    [field: SerializeField] public int EquippedCharacterId { get; set; }
    [field: SerializeField] public int Coin { get; set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public bool Mute { get; set; }
    [field: SerializeField] public bool Vibration { get; set; }
    public int Exp
    {
        get { return _exp; }

        set
        {
            _exp = value;
            if (_exp > MaxExp) Level++;
        }
    }
    public int MaxExp { get { return (Level + 1) * (Level + 1) * 10; } }

    [SerializeField] private int _exp;

    public UserData()
    {
        EquippedCharacterId = 0;
        Coin = 0;
        Mute = false;
        Vibration = true;
    }
}
