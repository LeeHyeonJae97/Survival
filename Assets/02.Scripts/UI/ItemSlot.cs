using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _button;

    // for new item (reward)
    public void Init(Item item)
    {
        ItemSO info = item.Info;

        bool contains = Player.Instance.ItemDic.ContainsKey(info.Id);
        int level = contains ? Player.Instance.ItemDic[info.Id].Level + 1 : 0;

        _slotImage.color = Grade.Colors[(int)info.Grade];
        _iconImage.sprite = info.Icon;
        _nameText.text = info.Names[level];
        _levelText.text = $"{level + 1}";
        _statImage.sprite = Stat.Infos[(int)info.Buffs[level].Buffs[item.Reinforced].Type].Icon;
        _statText.text = $"{info.Buffs[level].Buffs[item.Reinforced].Value}";
        _descriptionText.text = info.Descriptions[level];
        _button.interactable = true;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            UIFactory.Get<ConfirmUI>().Confirm("확실합니까?", () =>
            {
                // if already had, level up the item
                if (contains)
                {
                    Player.Instance.ItemDic[info.Id].LevelUp();
                }

                // if not, equip item newly
                else
                {
                    Player.Instance.Equip(new LiveItem(item));
                }

                // update ui
                UIFactory.Get<RewardUI>().SetActive(false);
                UIFactory.Get<NextWaveSelectionUI>().SetActive(true);
            });
        });
    }

    // for my item
    public void Init(LiveItem liveItem)
    {
        ItemSO item = liveItem.Item.Info;

        _slotImage.color = Grade.Colors[(int)item.Grade];
        _iconImage.sprite = item.Icon;
        _nameText.text = item.Names[liveItem.Level];
        _levelText.text = $"{liveItem.Level + 1}";
        _statImage.sprite = Stat.Infos[(int)item.Buffs[liveItem.Level].Buffs[liveItem.Item.Reinforced].Type].Icon;
        _statText.text = $"{item.Buffs[liveItem.Level].Buffs[liveItem.Item.Reinforced].Value}";
        _descriptionText.text = item.Descriptions[liveItem.Level];
        _button.interactable = false;
    }
}
