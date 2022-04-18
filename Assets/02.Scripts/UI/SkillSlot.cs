using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    private bool Enabled
    {
        set
        {
            if (value == _enabled) return;

            _enabled = value;
            _coverImage.SetActive(!value);
        }
    }

    [SerializeField] private Image _slotImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _coverImage;
    private bool _enabled;

    // for new skill (reward)
    public void Init(Skill skill)
    {
        SkillSO info = skill.Info;

        bool contains = Player.Instance.SkillDic.ContainsKey(info.Id);
        int level = contains ? Player.Instance.SkillDic[info.Id].Level + 1 : 0;

        _slotImage.color = Grade.Colors[(int)info.Grade];
        _iconImage.sprite = info.Icon;
        _nameText.text = info.Names[level];
        _levelText.text = $"{level + 1}";
        _damageText.text = $"{info.Stats[level].Stats[skill.Reinforced].Damage}";
        _cooldownText.text = $"{info.Stats[level].Stats[skill.Reinforced].Cooldown}";
        _priceText.text = $"{info.Prices[level]}";
        _descriptionText.text = info.Descriptions[level];
        _button.interactable = true;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            int price = info.Prices[level];

            UIFactory.Get<ConfirmUI>().Confirm($"{price}원. 확실합니까?", () =>
            {
                if (Player.Instance.Coin >= price)
                {
                    // if already had, level up the skill
                    if (contains)
                    {
                        Player.Instance.SkillDic[info.Id].LevelUp();
                    }

                    // if not, equip skill newly
                    else
                    {
                        Player.Instance.Equip(new LiveSkill(skill));
                    }

                    // spend coin
                    Player.Instance.Coin -= price;

                    Enabled = false;
                }
                else
                {
                    UIFactory.Get<AlertUI>().Alert($"{price - Player.Instance.Coin}원 부족합니다.");
                }
            });
        });

        Enabled = true;
    }

    // for my skill
    public void Init(LiveSkill liveSkill)
    {
        SkillSO skill = liveSkill.Skill.Info;

        _slotImage.color = Grade.Colors[(int)skill.Grade];
        _iconImage.sprite = skill.Icon;
        _nameText.text = skill.Names[liveSkill.Level];
        _levelText.text = $"{liveSkill.Level + 1}";
        _damageText.text = $"{skill.Stats[liveSkill.Level].Stats[liveSkill.Skill.Reinforced].Damage}";
        _cooldownText.text = $"{skill.Stats[liveSkill.Level].Stats[liveSkill.Skill.Reinforced].Cooldown}";
        _descriptionText.text = skill.Descriptions[liveSkill.Level];
        _button.interactable = false;

        Enabled = true;
    }
}
