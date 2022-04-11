using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character/Character")]
public class CharacterSO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public CharacterStatSO[] Stats { get; private set; }

    private void OnValidate()
    {
        if(Stats == null)
        {
            Stats = new CharacterStatSO[Character.MAX_REINFORCED];
        }
        else if(Stats.Length != Character.MAX_REINFORCED)
        {
            CharacterStatSO[] replace = new CharacterStatSO[Character.MAX_REINFORCED];

            for (int i=0; i<Stats.Length && i<Character.MAX_REINFORCED; i++)
            {
                replace[i] = Stats[i];
            }
            Stats = replace;
        }
    }
}
