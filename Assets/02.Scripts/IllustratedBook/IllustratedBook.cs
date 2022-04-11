using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DEPRECATED

[System.Serializable]
public class IllustratedBook
{
    [field: SerializeField] public bool[] Characters { get; private set; }
    [field: SerializeField] public bool[] Items { get; private set; }
    [field: SerializeField] public bool[] Skills { get; private set; }
    [field: SerializeField] public bool[] Potions { get; private set; }
    [field: SerializeField] public bool[] Enemies { get; private set; }

    public IllustratedBook(int characterCount, int itemCount, int skillCount, int potionCount, int enemyCount)
    {
        Characters = new bool[characterCount];
        Items = new bool[itemCount];
        Skills = new bool[skillCount];
        Potions = new bool[potionCount];
        Enemies = new bool[enemyCount];
    }
}
