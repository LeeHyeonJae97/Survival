using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WavePair
{
    [field: SerializeField] public WaveSO[] Waves { get; private set; }
    public WaveSO Selected { get { return Waves[SelectedIdx]; } }
    public int SelectedIdx { get; set; }

    public void OnValidate()
    {
        if (Waves != null)
        {
            Waves = new WaveSO[2] { Waves.Length > 0 ? Waves[0] : null, Waves.Length > 1 ? Waves[1] : null };
        }
    }

    public void Reset()
    {
        for (int i = 0; i < Waves.Length; i++) Waves[i].Reset();
    }
}
