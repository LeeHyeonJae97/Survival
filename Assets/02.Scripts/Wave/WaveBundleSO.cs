using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveBundle", menuName = "ScriptableObject/Wave/WaveBundle")]
public class WaveBundleSO : ScriptableObject
{
    [field: SerializeField] public bool Infinite { get; private set; }
    [field: SerializeField] public WaveSO[] Waves { get; private set; }
    public WaveSO Current { get { return Infinite ? Waves[0] : Waves[Index]; } }
    public WaveSO Next
    {
        get
        {
            if (Infinite)
            {
                Waves[0].Reset();
                return Waves[0];
            }
            else
            {
                return Index == Waves.Length ? null : Waves[Index++];
            }
        }
    }
    public int Index { get; private set; }

    private void OnValidate()
    {
        if (Infinite && Waves != null && Waves.Length > 1) Waves = new WaveSO[1] { Waves[0] };
    }

    public void Reset()
    {
        Index = 0;
        for (int i = 0; i < Waves.Length; i++) Waves[i].Reset();
    }
}
