using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _button;

    // for new skill (reward)
    public void Init(SkillSO skill)
    {
        bool contains = Player.Instance.SkillDic.ContainsKey(skill.Id);
        int level = contains ? Player.Instance.SkillDic[skill.Id].Level : 0;

        _slotImage.color = Grade.Colors[(int)skill.Grade];
        _iconImage.sprite = skill.Icon;
        _nameText.text = skill.Name;
        _levelText.text = $"{level + 1}";
        _damageText.text = $"{skill.Stats[level].Damage}";
        _cooldownText.text = $"{skill.Stats[level].Cooldown}";
        _descriptionText.text = skill.Description;
        _button.interactable = true;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            UIFactory.Get<ConfirmUI>().Confirm("확실합니까?", () =>
            {
                // if already had, level up the skill
                if (contains)
                {
                    Player.Instance.SkillDic[skill.Id].LevelUp();
                }

                // if not, equip skill newly
                else
                {
                    Player.Instance.Equip(new LiveSkill(skill));
                }

                // update ui
                UIFactory.Get<RewardUI>().SetActive(false);
                UIFactory.Get<NextWaveSelectionUI>().SetActive(true);
            });
        });
    }

    // for my skill
    public void Init(LiveSkill liveSkill)
    {
        SkillSO skill = liveSkill.Skill;

        _slotImage.color = Grade.Colors[(int)liveSkill.Skill.Grade];
        _iconImage.sprite = skill.Icon;
        _nameText.text = skill.Name;
        _levelText.text = $"{liveSkill.Level + 1}";
        _damageText.text = $"{skill.Stats[liveSkill.Level].Damage}";
        _cooldownText.text = $"{skill.Stats[liveSkill.Level].Cooldown}";
        _descriptionText.text = skill.Description;
        _button.interactable = false;
    }
}
