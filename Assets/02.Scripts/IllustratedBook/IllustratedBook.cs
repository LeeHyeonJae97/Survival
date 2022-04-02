using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IllustratedBook
{
    [field: SerializeField] public bool[] Items { get; private set; }
    [field: SerializeField] public bool[] Skills { get; private set; }
    [field: SerializeField] public bool[] Enemies { get; private set; }

    public IllustratedBook()
    {
        Items = new bool[ItemFactory.Count];
        Skills = new bool[SkillFactory.Count];
        Enemies = new bool[EnemyFactory.Count];
    }
}
