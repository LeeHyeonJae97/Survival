using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookSlot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _coverImage;

    public void Init(ItemSO item, bool registered)
    {
        _iconImage.sprite = item.Icon;
        _coverImage.SetActive(!registered);
    }

    public void Init(SkillSO skill, bool registered)
    {
        _iconImage.sprite = skill.Icon;
        _coverImage.SetActive(!registered);
    }

    public void Init(PotionSO potion, bool registered)
    {
        _iconImage.sprite = potion.Icon;
        _coverImage.SetActive(!registered);
    }

    public void Init(EnemySO enemy, bool registered)
    {
        _iconImage.sprite = enemy.Sprite;
        _coverImage.SetActive(!registered);
    }
}
