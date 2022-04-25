using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrowdControlType { Fire, Ice, Poison, Wind, Curse, Explosion }

[System.Serializable]
public struct CrowdControlInfo
{
    public const int MAX_LEVEL = 2;

    [field: SerializeField] public CrowdControlType Type { get; private set; }
    //[field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Percent { get; private set; }
    [field: SerializeField] public float[] Durations { get; private set; }
    [field: SerializeField] public float[] Values { get; private set; }
    [field: SerializeField] public float[] Intervals { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
}
