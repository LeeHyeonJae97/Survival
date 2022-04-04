using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IllustratedBook
{
    [field: SerializeField] public bool[] Items { get; private set; }
    [field: SerializeField] public bool[] Skills { get; private set; }
    [field: SerializeField] public bool[] Potions { get; private set; }
    [field: SerializeField] public bool[] Enemies { get; private set; }

    public IllustratedBook(int itemCount, int skillCount, int potionCount, int enemyCount)
    {
        Items = new bool[itemCount];
        Skills = new bool[skillCount];
        Potions = new bool[potionCount];
        Enemies = new bool[enemyCount];
    }
}
