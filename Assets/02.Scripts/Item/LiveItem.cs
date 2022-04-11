using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveItem
{
    public int Level { get; private set; }
    public Item Item { get; private set; }

    public LiveItem(Item item)
    {
        Level = 0;
        Item = item;
    }

    public void LevelUp()
    {
        Level++;
    }
}
