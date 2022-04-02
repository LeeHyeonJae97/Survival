using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatInfo", menuName = "ScriptableObject/Stat/StatInfo")]
public class StatInfoSO : ScriptableObject
{
    [field: SerializeField] public StatType Type { get; private set; }
    [field: SerializeField] public Vector2Int Range { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
