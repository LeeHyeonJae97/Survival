using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType { Item, Skill, Potion }

public class Reward
{
    public static RewardInfo[] Infos
    {
        get
        {
            if (_infos == null)
            {
                List<RewardInfo> infos = new List<RewardInfo>(Resources.LoadAll<RewardInfo>("RewardInfo"));

                if (infos != null)
                {
                    infos.Sort((l, r) => l.Type.CompareTo(r.Type));
                    _infos = infos.ToArray();
                }
            }
            return _infos;
        }
    }

    private static RewardInfo[] _infos;
}
