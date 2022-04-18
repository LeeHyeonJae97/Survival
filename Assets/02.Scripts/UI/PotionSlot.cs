using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionSlot : MonoBehaviour
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
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _coverImage;
    private bool _enabled;

    public void Init(Potion potion)
    {
        PotionSO info = potion.Info;

        _slotImage.color = Grade.Colors[(int)info.Grade];
        _iconImage.sprite = info.Icon;
        _nameText.text = info.Name;
        _statImage.sprite = Stat.Infos[(int)info.Buff.Type].Icon;
        _statText.text = $"{info.Buff.Value}";
        _durationText.text = $"{info.Duration}";
        _priceText.text = $"{info.Price}";
        _descriptionText.text = info.Description;
        _button.interactable = true;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            int price = info.Price;

            UIFactory.Get<ConfirmUI>().Confirm($"{price}원. 확실합니까?", () =>
            {
                if (Player.Instance.Coin >= price)
                {
                    Player.Instance.Equip(new LivePotion(potion));
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

    public void Init(LivePotion livePotion)
    {
        PotionSO potion = livePotion.Potion.Info;

        _slotImage.color = Grade.Colors[(int)potion.Grade];
        _iconImage.sprite = potion.Icon;
        _nameText.text = potion.Name;
        _statImage.sprite = Stat.Infos[(int)potion.Buff.Type].Icon;
        _statText.text = $"{potion.Buff.Value}";
        _durationText.text = $"{livePotion.Duration}";
        _button.interactable = false;

        Enabled = true;
    }
}
