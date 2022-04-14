using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item/Item")]
public class ItemSO : ScriptableObject
{
    public const int MAX_LEVEL = 3;

    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string[] Names { get; private set; }
    [field: SerializeField] public GradeType Grade { get; private set; }
    [field: SerializeField] public string[] Descriptions { get; private set; }
    [field: SerializeField] public ItemBuffSO[] Buffs { get; private set; }

    private void OnValidate()
    {
        if (Names == null)
        {
            Names = new string[MAX_LEVEL];
        }
        else if (Names.Length != MAX_LEVEL)
        {
            string[] replace = new string[MAX_LEVEL];

            for (int i = 0; i < Names.Length && i < MAX_LEVEL; i++)
            {
                replace[i] = Names[i];
            }
            Names = replace;
        }

        if (Descriptions == null)
        {
            Descriptions = new string[MAX_LEVEL];
        }
        else if (Descriptions.Length != MAX_LEVEL)
        {
            string[] replace = new string[MAX_LEVEL];

            for (int i = 0; i < Descriptions.Length && i < MAX_LEVEL; i++)
            {
                replace[i] = Descriptions[i];
            }
            Descriptions = replace;
        }

        if (Buffs == null)
        {
            Buffs = new ItemBuffSO[MAX_LEVEL];
        }
        else if (Buffs.Length != MAX_LEVEL)
        {
            ItemBuffSO[] replace = new ItemBuffSO[MAX_LEVEL];

            for (int i = 0; i < Buffs.Length && i < MAX_LEVEL; i++)
            {
                replace[i] = i < Buffs.Length ? Buffs[i] : null;
            }
            Buffs = replace;
        }
    }
}
