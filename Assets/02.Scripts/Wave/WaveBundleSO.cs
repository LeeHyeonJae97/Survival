using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveBundle", menuName = "ScriptableObject/Wave/WaveBundle")]
public class WaveBundleSO : ScriptableObject
{
    [field: SerializeField] public bool Infinite { get; private set; }
    [field: SerializeField] public WavePair[] Waves { get; private set; }
    public WaveSO Current { get { return Infinite ? Waves[0].Selected : Waves[Index].Selected; } }
    public WaveSO Next { get { return Infinite ? Waves[0].Selected : Index == Waves.Length ? null : Waves[Index++].Selected; } }
    public int Index { get; private set; }

    private void OnValidate()
    {
        if (Infinite && Waves != null && Waves.Length > 1) Waves = new WavePair[1] { Waves[0] };

        for (int i = 0; i < Waves.Length; i++) Waves[i].OnValidate();
    }

    public void Reset()
    {
        Index = 0;
        for (int i = 0; i < Waves.Length; i++) Waves[i].Reset();
    }
}
