using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
