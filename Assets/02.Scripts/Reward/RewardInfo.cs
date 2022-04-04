using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardInfo", menuName = "ScriptableObject/Reward/RewardInfo")]
public class RewardInfo : ScriptableObject
{
    [field: SerializeField] public RewardType Type { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
