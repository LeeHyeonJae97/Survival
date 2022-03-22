using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomExtension
{
    public static int percent { get { return Random.Range(0, 100); } }

    public static bool CheckPercent(int value)
    {
        return percent < value;
    }
}
