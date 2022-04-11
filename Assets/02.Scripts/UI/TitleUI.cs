using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : UI
{
    [SerializeField] private Button _adventureButton;
    [SerializeField] private Button _characterButton;
    [SerializeField] private Button _illustratedBookButton;
    [SerializeField] private Button _settingsButton;

    protected override void Awake()
    {
        base.Awake();

        _adventureButton.onClick.AddListener(() => OnClickTabButton(0));
        _characterButton.onClick.AddListener(() => OnClickTabButton(1));
        _illustratedBookButton.onClick.AddListener(() => OnClickTabButton(2));
        _settingsButton.onClick.AddListener(() => OnClickTabButton(3));
    }

    protected override void OnSetActive(bool value)
    {
        if (value) OnClickTabButton(0);
    }

    private void OnClickTabButton(int index)
    {
        UIFactory.Get<AdventureUI>().SetActive(index == 0);
        UIFactory.Get<CharacterUI>().SetActive(index == 1);
        UIFactory.Get<IllustratedBookUI>().SetActive(index == 2);
        UIFactory.Get<SettingsUI>().SetActive(index == 3);

        _adventureButton.targetGraphic.color = index == 0 ? _adventureButton.colors.normalColor : _adventureButton.colors.disabledColor;
        _characterButton.targetGraphic.color = index == 1 ? _characterButton.colors.normalColor : _characterButton.colors.disabledColor;
        _illustratedBookButton.targetGraphic.color = index == 2 ? _illustratedBookButton.colors.normalColor : _illustratedBookButton.colors.disabledColor;
        _settingsButton.targetGraphic.color = index == 3 ? _settingsButton.colors.normalColor : _settingsButton.colors.disabledColor;
    }
}