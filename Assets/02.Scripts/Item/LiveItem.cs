using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveItem
{
    public int Level { get; private set; }
    public ItemSO Item { get; private set; }

    public LiveItem(ItemSO item)
    {
        Level = 0;
        Item = item;
    }

    public void LevelUp()
    {
        Level++;
    }
}
