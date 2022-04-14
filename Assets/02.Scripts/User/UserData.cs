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
    [field: SerializeField] public bool Mute { get; set; }
    [field: SerializeField] public bool Vibration { get; set; }
    
    public UserData()
    {
        EquippedCharacterId = 0;
        Coin = 0;
        Mute = false;
        Vibration = true;
    }
}
