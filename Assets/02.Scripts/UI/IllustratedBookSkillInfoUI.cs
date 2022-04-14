using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookSkillInfoUI : UI
{
    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private Image _skillImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _reinforcedText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _reinforcedDamageText;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private TextMeshProUGUI _reinforcedCooldownText;
    [SerializeField] private Button _reinforceButton;
    [SerializeField] private Button _confirmReinforceButton;
    [SerializeField] private Button _cancelReinforceButton;
    [SerializeField] private Button _coverButton;
    private Skill _skill;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int index = i;
            _levelButtons[i].onClick.AddListener(() => OnClickLevelButton(index));
        }
        _reinforceButton.onClick.AddListener(OnClickReinforceButton);
        _confirmReinforceButton.onClick.AddListener(OnClickConfirmReinforceButton);
        _cancelReinforceButton.onClick.AddListener(OnClickCancelReinforceButton);
        _coverButton.onClick.AddListener(OnClickCoverButton);
    }

    public void SetActive(bool value, Skill skill)
    {
        if (value)
        {
            _skill = skill;

            OnClickLevelButton(0);

            OnClickCancelReinforceButton();
        }

        SetActive(value);
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnClickLevelButton(int level)
    {
        if (_skill == null) return;

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].targetGraphic.color = i == level ? _levelButtons[i].colors.normalColor : _levelButtons[i].colors.disabledColor;
        }

        _skillImage.sprite = _skill.Info.Icon;

        _nameText.text = _skill.Info.Names[level];

        _reinforcedText.text = $"°­ÇØÁü+{_skill.Reinforced}";

        _descriptionText.text = _skill.Info.Descriptions[level];

        _damageText.text = $"{_skill.Info.Stats[level].Stats[_skill.Reinforced].Damage}";
        _reinforcedDamageText.gameObject.SetActive(false);
        _reinforcedDamageText.text = $"{_skill.Info.Stats[level].Stats[_skill.Reinforced + 1].Damage}";
        _cooldownText.text = $"{_skill.Info.Stats[level].Stats[_skill.Reinforced].Cooldown}";
        _reinforcedCooldownText.gameObject.SetActive(false);
        _reinforcedCooldownText.text = $"{_skill.Info.Stats[level].Stats[_skill.Reinforced + 1].Cooldown}";
    }

    private void OnClickReinforceButton()
    {
        if (_skill == null) return;

        // show reinforced stat texts
        _reinforcedDamageText.gameObject.SetActive(true);
        _reinforcedCooldownText.gameObject.SetActive(true);

        // hide reinforce button
        _reinforceButton.gameObject.SetActive(false);

        // show confirm reinforce button and cancel reinforce button
        _confirmReinforceButton.gameObject.SetActive(true);
        _cancelReinforceButton.gameObject.SetActive(true);
    }

    private void OnClickConfirmReinforceButton()
    {
        if (_skill == null) return;

        // reinforce
        _skill.Reinforce();

        // update ui
        OnClickCancelReinforceButton();

        // NOTICE :
        // need to save updated information in local file?
    }

    private void OnClickCancelReinforceButton()
    {
        if (_skill == null) return;

        // hide reinforced stat texts
        _reinforcedDamageText.gameObject.SetActive(false);
        _reinforcedCooldownText.gameObject.SetActive(false);

        // hide confirm reinforce button and cancel reinforce button
        _confirmReinforceButton.gameObject.SetActive(false);
        _cancelReinforceButton.gameObject.SetActive(false);

        // show reinforce button
        _reinforceButton.gameObject.SetActive(true);
    }

    private void OnClickCoverButton()
    {
        SetActive(false);
    }
}
