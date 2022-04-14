using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookItemInfoUI : UI
{
    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _reinforcedText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _reinforcedStatText;
    [SerializeField] private Button _reinforceButton;
    [SerializeField] private Button _confirmReinforceButton;
    [SerializeField] private Button _cancelReinforceButton;
    [SerializeField] private Button _coverButton;
    private Item _item;

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

    public void SetActive(bool value, Item item)
    {
        if (value)
        {
            _item = item;

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
        if (_item == null) return;

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].targetGraphic.color = i == level ? _levelButtons[i].colors.normalColor : _levelButtons[i].colors.disabledColor;
        }

        _itemImage.sprite = _item.Info.Icon;

        _nameText.text = _item.Info.Names[level];

        _reinforcedText.text = $"°­ÇØÁü+{_item.Reinforced}";

        _descriptionText.text = _item.Info.Descriptions[level];

        _statImage.sprite = Stat.Infos[(int)_item.Info.Buffs[level].Buffs[_item.Reinforced].Type].Icon;
        _statText.text = $"{_item.Info.Buffs[level].Buffs[_item.Reinforced].Value}";
        _reinforcedStatText.gameObject.SetActive(false);
        _reinforcedStatText.text = $"{_item.Info.Buffs[level].Buffs[_item.Reinforced + 1].Value}";
    }

    private void OnClickReinforceButton()
    {
        if (_item == null) return;

        // show reinforced stat text
        _reinforcedStatText.gameObject.SetActive(true);

        // hide reinforce button
        _reinforceButton.gameObject.SetActive(false);

        // show confirm reinforce button and cancel reinforce button
        _confirmReinforceButton.gameObject.SetActive(true);
        _cancelReinforceButton.gameObject.SetActive(true);
    }

    private void OnClickConfirmReinforceButton()
    {
        if (_item == null) return;

        // reinforce
        _item.Reinforce();

        // update ui
        OnClickCancelReinforceButton();

        // NOTICE :
        // need to save updated information in local file?
    }

    private void OnClickCancelReinforceButton()
    {
        if (_item == null) return;

        // hide reinforced stat text
        _reinforcedStatText.gameObject.SetActive(false);

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
