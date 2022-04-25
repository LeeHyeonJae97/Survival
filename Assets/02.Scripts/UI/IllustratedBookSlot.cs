using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookSlot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _coverImage;
    [SerializeField] private Button _button;

    public void Init(Character character)
    {
        _iconImage.sprite = character.Info.Sprite;
        _coverImage.SetActive(!character.Registered);
        _button.interactable = character.Registered;
        _button.onClick.AddListener(() => UIFactory.Get<IllustratedBookCharacterInfoUI>().SetActive(true, character));
    }

    public void Init(Item item)
    {
        _iconImage.sprite = item.Info.Icon;
        _coverImage.SetActive(!item.Registered);
        _button.interactable = item.Registered;
        _button.onClick.AddListener(() => UIFactory.Get<IllustratedBookItemInfoUI>().SetActive(true, item));
    }

    public void Init(Skill skill)
    {
        _iconImage.sprite = skill.Info.Icon;
        _coverImage.SetActive(!skill.Registered);
        _button.interactable = skill.Registered;
        _button.onClick.AddListener(() => UIFactory.Get<IllustratedBookSkillInfoUI>().SetActive(true, skill));
    }

    public void Init(Potion potion)
    {
        _iconImage.sprite = potion.Info.Icon;
        _coverImage.SetActive(!potion.Registered);
        _button.interactable = potion.Registered;
        _button.onClick.AddListener(() => UIFactory.Get<IllustratedBookPotionInfoUI>().SetActive(true, potion));
    }

    public void Init(Enemy enemy)
    {
        _iconImage.sprite = enemy.Info.Sprite;
        _coverImage.SetActive(!enemy.Registered);
        _button.interactable = enemy.Registered;
        _button.onClick.AddListener(() => UIFactory.Get<IllustratedBookEnemyInfoUI>().SetActive(true, enemy));
    }
}
