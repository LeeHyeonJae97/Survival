using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedBookUI : UI
{
    [SerializeField] private UserSO _user;
    [SerializeField] private Transform _itemHolder;
    [SerializeField] private Transform _skillHolder;
    [SerializeField] private Transform _enemyHolder;

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            IllustratedBook ib = _user.IllustratedBook;

            // initialize item illustrated book slots
            for (int i = 0; i < _itemHolder.childCount && i < ib.Items.Length && i < ItemFactory.Count; i++)
            {
                _itemHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(ItemFactory.Get(i), ib.Items[i]);
            }

            // initialize skill illustrated book slots
            for (int i = 0; i < _skillHolder.childCount && i < ib.Skills.Length && i < SkillFactory.Count; i++)
            {
                _skillHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(SkillFactory.Get(i), ib.Skills[i]);
            }

            // initialize enemy illustrated book slots
            for (int i = 0; i < _enemyHolder.childCount && i < ib.Enemies.Length && i < EnemyFactory.Count; i++)
            {
                _enemyHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(EnemyFactory.Get(i), ib.Enemies[i]);
            }
        }
    }
}
