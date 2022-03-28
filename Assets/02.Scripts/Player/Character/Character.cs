using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character/Character")]
public class Character : ScriptableObject
{
    [field: SerializeField] public int HP { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
