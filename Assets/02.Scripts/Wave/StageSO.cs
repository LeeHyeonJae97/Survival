using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObject/Stage/Stage")]
public class StageSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public WaveSO[] Waves { get; private set; }
    public WaveSO Next
    {
        get
        {
            Current = Waves == null || Waves.Length == 0 || Index == Waves.Length ? null : Waves[Index++];
            return Current;
        }
    }
    public WaveSO Current { get; private set; }
    public int Index { get; private set; }

    public void Reset()
    {
        Index = 0;
        for (int i = 0; i < Waves.Length; i++) Waves[i].Reset();
    }
}
