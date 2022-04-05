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
    public void Init(ItemSO item)
    {
        bool contains = Player.Instance.ItemDic.ContainsKey(item.Id);
        int level = contains ? Player.Instance.ItemDic[item.Id].Level + 1 : 0;

        _slotImage.color = Grade.Colors[(int)item.Grade];
        _iconImage.sprite = item.Icon;
        _nameText.text = item.Name;
        _levelText.text = $"{level + 1}";
        _statImage.sprite = Stat.Infos[(int)item.Buff.Type].Icon;
        _statText.text = $"{item.Buff.Values[level]}";
        _descriptionText.text = item.Description;
        _button.interactable = true;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            UIFactory.Get<ConfirmUI>().Confirm("Ȯ���մϱ�?", () =>
            {
                // if already had, level up the item
                if (contains)
                {
                    Player.Instance.ItemDic[item.Id].LevelUp();
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
        ItemSO item = liveItem.Item;

        _slotImage.color = Grade.Colors[(int)liveItem.Item.Grade];
        _iconImage.sprite = item.Icon;
        _nameText.text = item.Name;
        _levelText.text = $"{liveItem.Level + 1}";
        _statImage.sprite = Stat.Infos[(int)item.Buff.Type].Icon;
        _statText.text = $"{item.Buff.Values[liveItem.Level]}";
        _descriptionText.text = item.Description;
        _button.interactable = false;
    }
}