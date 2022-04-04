using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveSelectionUI : UI
{
    [SerializeField] private NextWaveInfoSlot[] _slots;

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            // TODO :
            // implement for story mode

            // for infinite mode
            for (int i = 0; i < _slots.Length; i++)
            {
                string name = "";
                RewardType type = RewardType.Item;
                float duration = Random.Range(5, 10);
                float interval = Random.Range(1, 2);
                EnemySO[] enemies = EnemyFactory.GetRandom(Random.Range(3, 5));

                _slots[i].Init(name, type, duration, interval, enemies);
            }
        }
    }
}
