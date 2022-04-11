using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookCharacterInfoUI : UI
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _reinforcedText;
    [SerializeField] private TextMeshProUGUI _abilityText;
    [SerializeField] private TextMeshProUGUI[] _statTexts;
    [SerializeField] private TextMeshProUGUI[] _reinforcedStatTexts;
    [SerializeField] private Button _reinforceButton;
    [SerializeField] private Button _confirmReinforceButton;
    [SerializeField] private Button _cancelReinforceButton;
    [SerializeField] private Button _equipButton;

    protected override void Awake()
    {
        _reinforceButton.onClick.AddListener(OnClickReinforceButton);
        _confirmReinforceButton.onClick.AddListener(OnClickConfirmReinforceButton);
        _cancelReinforceButton.onClick.AddListener(OnClickCancelReinforceButton);
        _equipButton.onClick.AddListener(OnClickEquipButton);
    }

    public void SetActive(bool value, Character character)
    {
        if (value)
        {
            // CONTINUE :

            _characterImage.sprite = character.Info.Sprite;
            _nameText.text = character.Info.Name;
        }

        SetActive(value);
    }

    protected override void OnSetActive(bool value)
    {
        throw new System.NotImplementedException();
    }

    private void OnClickReinforceButton()
    {
        // show reinforced stat texts

        // hide reinforce button and equip button

        // show confirm reinforce button and cancel reinforce button
    }

    private void OnClickConfirmReinforceButton()
    {
        // reinforce

        // save updated information in local file
    }

    private void OnClickCancelReinforceButton()
    {
        // hide reinforced stat texts

        // hide confirm reinforce button and cancel reinforce button

        // show reinforce button and equip button
    }

    private void OnClickEquipButton()
    {
        // save reference at 'Player'

        // save updated information in local file
    }
}
