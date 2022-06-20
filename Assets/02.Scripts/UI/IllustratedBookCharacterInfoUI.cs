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
    [SerializeField] private TextMeshProUGUI _equipButtonText;
    [SerializeField] private Button _reinforceButton;
    [SerializeField] private Button _confirmReinforceButton;
    [SerializeField] private Button _cancelReinforceButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _coverButton;
    private Character _character;

    protected override void Awake()
    {
        base.Awake();

        _reinforceButton.onClick.AddListener(OnClickReinforceButton);
        _confirmReinforceButton.onClick.AddListener(OnClickConfirmReinforceButton);
        _cancelReinforceButton.onClick.AddListener(OnClickCancelReinforceButton);
        _equipButton.onClick.AddListener(OnClickEquipButton);
        _coverButton.onClick.AddListener(OnClickCoverButton);
    }

    public void SetActive(bool value, Character character)
    {
        if (value)
        {
            _character = character;

            _characterImage.sprite = character.Info.Sprite;

            _nameText.text = character.Info.Name;

            _reinforcedText.text = $"강해짐+{character.Reinforced}";

            _abilityText.text = "능력";

            for (int i = 0; i < _statTexts.Length; i++)
            {
                _statTexts[i].text = $"{character.Info.Stats[character.Reinforced].Stats[i].Value}";
            }

            for (int i = 0; i < _statTexts.Length; i++)
            {
                _reinforcedStatTexts[i].gameObject.SetActive(false);
                _reinforcedStatTexts[i].text = $"{character.Info.Stats[character.Reinforced + 1].Stats[i].Value}";
            }

            bool equipped = character.Info.Id == GameManager.GetInstance().User.EquippedCharacterId;
            _equipButtonText.text = equipped ? "함께하는 중" : "함께하기";
            _equipButton.interactable = !equipped;

            OnClickCancelReinforceButton();
        }

        SetActive(value);
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnClickReinforceButton()
    {
        if (_character == null) return;

        // show reinforced stat texts
        for (int i = 0; i < _reinforcedStatTexts.Length; i++) _reinforcedStatTexts[i].gameObject.SetActive(true);

        // hide reinforce button and equip button
        _reinforceButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);

        // show confirm reinforce button and cancel reinforce button
        _confirmReinforceButton.gameObject.SetActive(true);
        _cancelReinforceButton.gameObject.SetActive(true);
    }

    private void OnClickConfirmReinforceButton()
    {
        if (_character == null) return;

        // reinforce
        _character.Reinforce();

        // update ui
        OnClickCancelReinforceButton();

        // NOTICE :
        // need to save updated information in local file?
    }

    private void OnClickCancelReinforceButton()
    {
        if (_character == null) return;

        // hide reinforced stat texts
        for (int i = 0; i < _reinforcedStatTexts.Length; i++) _reinforcedStatTexts[i].gameObject.SetActive(false);

        // hide confirm reinforce button and cancel reinforce button
        _confirmReinforceButton.gameObject.SetActive(false);
        _cancelReinforceButton.gameObject.SetActive(false);

        // show reinforce button and equip button
        _reinforceButton.gameObject.SetActive(true);
        _equipButton.gameObject.SetActive(true);
    }

    private void OnClickEquipButton()
    {
        if (_character == null) return;

        // save reference at 'Player'
        GameManager.GetInstance().User.EquippedCharacterId = _character.Info.Id;

        // update ui
        _equipButtonText.text = "함께하는 중";
        _equipButton.interactable = false;

        // NOTICE :
        // need to save updated information in local file?
    }

    private void OnClickCoverButton()
    {
        SetActive(false);
    }
}
