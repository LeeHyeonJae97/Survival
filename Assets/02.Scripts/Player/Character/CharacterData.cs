using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public const string DIR_PATH = "CharacterData";
    public const string FILE_PATH = "characterData.json";

    [field: SerializeField] public Character[] Characters { get; private set; }

    public CharacterData()
    {
        Characters = new Character[CharacterFactory.Count];
        for (int i = 0; i < Characters.Length; i++) Characters[i] = new Character();
    }

    public CharacterData(Character[] characters)
    {
        this.Characters = characters;
    }
}
