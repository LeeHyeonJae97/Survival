using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionSlot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private Button _button;

    public void Init(PotionSO potion)
    {
        _iconImage.sprite = potion.Icon;
        _nameText.text = potion.Name;
        _statImage.sprite = Stat.Infos[(int)potion.Buff.Type].Icon;
        _statText.text = $"{potion.Buff.Value}";
        _durationText.text = $"{potion.Duration}";
        _button.interactable = true;

        _button.onClick.AddListener(() =>
        {
            Player.Instance.Equip(new LivePotion(potion));
        });
    }

    public void Init(LivePotion livePotion)
    {
        _iconImage.sprite = livePotion.Potion.Icon;
        _nameText.text = livePotion.Potion.Name;
        _statImage.sprite = Stat.Infos[(int)livePotion.Potion.Buff.Type].Icon;
        _statText.text = $"{livePotion.Potion.Buff.Value}";
        _durationText.text = $"{livePotion.Duration}";
        _button.interactable = false;
    }
}
