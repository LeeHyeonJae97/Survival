using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public const string DIR_PATH = "ItemData";
    public const string FILE_PATH = "itemData.json";

    [field: SerializeField] public Item[] Items { get; private set; }

    public ItemData()
    {
        Items = new Item[ItemFactory.Count];
        for (int i = 0; i < Items.Length; i++) Items[i] = new Item();
    }

    public ItemData(Item[] items)
    {
        this.Items = items;
    }
}