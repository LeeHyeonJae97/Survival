using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveInfo", menuName = "ScriptableObject/Spawn/WaveInfo")]
public class WaveSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public RewardType RewardType { get; private set; }
    [field: SerializeField] public EnemySpawning[] Spawnings { get; private set; }
    public EnemySpawning Next
    {
        get { return _idx == Spawnings.Length ? null : Spawnings[_idx++]; }
    }
    public float Duration
    {
        get
        {
            float duration = 0;

            for (int i = 0; i < Spawnings.Length; i++) duration += Spawnings[i].Duration;
            return duration;
        }
    }

    private int _idx;

    public void Init()
    {
        _idx = 0;
    }

    public void Init(string name, RewardType rewardType, float duration, float interval, EnemySO[] enemies)
    {
        Name = name;
        RewardType = rewardType;
        (Spawnings[0] as EnemySpawningRandom).Init(duration, interval, enemies);
    }
}
