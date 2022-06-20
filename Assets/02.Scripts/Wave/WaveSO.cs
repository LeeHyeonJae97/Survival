using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObject/Stage/Wave")]
public class WaveSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] [field: TextArea] public string Description { get; private set; }
    [field: SerializeField] public EnemySpawning[] Spawnings { get; private set; }
    public EnemySpawning Next { get { return _idx == Spawnings.Length ? null : Spawnings[_idx++]; } }
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

    public void Reset()
    {
        _idx = 0;
    }
}
