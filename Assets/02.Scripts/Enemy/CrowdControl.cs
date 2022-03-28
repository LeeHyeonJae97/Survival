using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrowdControlType { Fire, Ice, Poison}

public class CrowdControl
{
    public CrowdControlType Type { get; private set; }
    public int Level { get; set; }
    public float Duration { get; set; }
    public bool Infinite { get; set; }
    public bool IsActive => Duration > 0 || Infinite;

    public CrowdControl(CrowdControlInfo cc)
    {
        Type = cc.Type;
        Level = 0;
        Duration = cc.Durations[Level];
        Infinite = false;
    }

    public void Reset(CrowdControlInfo cc)
    {
        Level = 0;
        Duration = cc.Durations[Level];
    }
}

[System.Serializable]
public struct CrowdControlInfo
{
    public const int MAX_LEVEL = 2;

    [field: SerializeField] public CrowdControlType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Percent { get; private set; }
    [field: SerializeField] public float[] Durations { get; private set; }
    [field: SerializeField] public float[] Values { get; private set; }
    [field: SerializeField] public float[] Intervals { get; private set; }
}
