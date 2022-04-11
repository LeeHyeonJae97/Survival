using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionData
{
    public const string DIR_PATH = "PotionData";
    public const string FILE_PATH = "potionData.json";

    [field: SerializeField] public Potion[] Potions { get; private set; }

    public PotionData()
    {
        Potions = new Potion[PotionFactory.Count];
        for (int i = 0; i < Potions.Length; i++) Potions[i] = new Potion();
    }

    public PotionData(Potion[] potions)
    {
        this.Potions = potions;
    }
}
