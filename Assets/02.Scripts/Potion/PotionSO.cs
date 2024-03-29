using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion" ,menuName = "ScriptableObject/Potion/Potion")]
public class PotionSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public GradeType Grade { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Duration { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public PotionBuff Buff { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
