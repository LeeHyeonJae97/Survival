using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionSlot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private Button _button;

    public void Init(Potion potion)
    {
        PotionSO info = potion.Info;
            
        _slotImage.color = Grade.Colors[(int)info.Grade];
        _iconImage.sprite = info.Icon;
        _nameText.text = info.Name;
        _statImage.sprite = Stat.Infos[(int)info.Buff.Type].Icon;
        _statText.text = $"{info.Buff.Value}";
        _durationText.text = $"{info.Duration}";
        _button.interactable = true;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            UIFactory.Get<ConfirmUI>().Confirm(("확실합니까?"), () =>
            {
                Player.Instance.Equip(new LivePotion(potion));

                // update ui
                UIFactory.Get<RewardUI>().SetActive(false);
                UIFactory.Get<NextWaveSelectionUI>().SetActive(true);
            });
        });
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
    }
}
