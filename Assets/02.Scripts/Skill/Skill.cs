using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public const int MAX_REINFORCED = 10;

    [field: SerializeField] public int Reinforced { get; private set; }
    [field: SerializeField] public bool Registered { get; private set; }
    public SkillSO Info { get; private set; }

    public Skill()
    {
        Reinforced = 0;
        Registered = false;
    }

    public void Init(SkillSO info)
    {
        Info = info;
    }

    public void Reinforce()
    {
        Reinforced++;
    }

    public void Register()
    {
        Registered = true;
    }
}
