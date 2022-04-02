using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveStat
{
    private int[] _bases;
    // for only potion buffs
    private int[] _buffs;
    private int[] _finals;
    private bool[] _isDirties;

    public int this[int index]
    {
        get
        {
            if (_isDirties[index]) _finals[index] = (int)(_bases[index] * (1 + (float)_buffs[index] / 100));
            return _finals[index];
        }
    }

    public LiveStat(Stat[] stats)
    {
        _bases = new int[stats.Length];
        _buffs = new int[stats.Length];
        _finals = new int[stats.Length];
        _isDirties = new bool[stats.Length];

        for (int i = 0; i < stats.Length; i++)
        {
            _bases[i] = stats[i].Value;
            _buffs[i] = 0;
            _isDirties[i] = true;
        }
    }

    public void Buffed(ItemBuff buff, int level)
    {
        _bases[(int)buff.Type] += buff.Values[level];
    }

    public void Buffed(PotionBuff buff)
    {
        _buffs[(int)buff.Type] += buff.Value;
    }

    public void Debuffed(PotionBuff buff)
    {
        _buffs[(int)buff.Type] -= buff.Value;
    }
}
