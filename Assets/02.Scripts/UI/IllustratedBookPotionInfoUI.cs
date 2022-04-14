using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookPotionInfoUI : UI
{
    [SerializeField] private Image _potionImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private Button _coverButton;

    protected override void Awake()
    {
        base.Awake();

        _coverButton.onClick.AddListener(OnClickCoverButton);
    }

    public void SetActive(bool value, Potion potion)
    {
        if (value)
        {
            _potionImage.sprite = potion.Info.Icon;

            _nameText.text = potion.Info.Name;

            _descriptionText.text = potion.Info.Description;

            _statImage.sprite = Stat.Infos[(int)potion.Info.Buff.Type].Icon;
            _statText.text = $"{potion.Info.Buff.Value}";

            _durationText.text = $"{potion.Info.Duration}";
        }

        SetActive(value);
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnClickCoverButton()
    {
        SetActive(false);
    }
}
