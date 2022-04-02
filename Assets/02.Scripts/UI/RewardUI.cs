using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UI
{
    // CONTINUE :
    // holder to be set active and holder for slots are different now and there's two options
    //
    // 1. stat, item, skill, potion, all have scroll view and only change the content of scroll view.
    //    if so, the content of scroll view should be set by script when changed.
    //
    // 2. reference each holder

    [SerializeField] private Button _rewardTabButton;
    [SerializeField] private Button _myInfoTabButton;
    [SerializeField] private GameObject _rewardHolder;
    [SerializeField] private GameObject _myInfoHolder;
    [SerializeField] private Image _characterImage;
    [SerializeField] private Image[] _statImages;
    [SerializeField] private TextMeshProUGUI[] _statTexts;
    [SerializeField] private GameObject _statHolder;
    [SerializeField] private Transform _itemHolder;
    [SerializeField] private Transform _skillHolder;
    [SerializeField] private Transform _potionHolder;
    [SerializeField] private SkillSlot _skillSlotPrefab;
    [SerializeField] private ItemSlot _itemSlotPrefab;
    [SerializeField] private PotionSlot _potionSlotPrefab;
    [SerializeField] private Button _statTabButton;
    [SerializeField] private Button _itemTabButton;
    [SerializeField] private Button _skillTabButton;
    [SerializeField] private Button _potionTabButton;

    protected override void Awake()
    {
        base.Awake();

        _rewardTabButton.onClick.AddListener(() => OnClickTabButton(0));
        _myInfoTabButton.onClick.AddListener(() => OnClickTabButton(1));

        _statTabButton.onClick.AddListener(() => OnClickSubTabButton(0));
        _itemTabButton.onClick.AddListener(() => OnClickSubTabButton(1));
        _skillTabButton.onClick.AddListener(() => OnClickSubTabButton(2));
        _potionTabButton.onClick.AddListener(() => OnClickTabButton(3));
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            // set reward infos
            //
            //
            //
            //

            // set my infos

            Player player = Player.Instance;

            // set character image
            _characterImage.sprite = player.Character.Sprite;

            // set my stats
            for (int i = 0; i < _statImages.Length && i < _statTexts.Length; i++)
            {
                _statImages[i].sprite = Stat.Infos[i].Icon;
                _statTexts[i].text = $"{player.Stats[i]}";
            }

            // set my skills
            List<LiveSkill> skills = new List<LiveSkill>(player.SkillDic.Values);

            for (int i = 0; i < _skillHolder.childCount || i < skills.Count; i++)
            {
                if (i < _skillHolder.childCount)
                {
                    _skillHolder.GetChild(i).GetComponent<SkillSlot>().Init(skills[i]);
                }
                else
                {
                    Instantiate(_skillSlotPrefab, _skillHolder).Init(skills[i]);
                }
            }

            // set my items
            List<LiveItem> items = new List<LiveItem>(player.ItemDic.Values);

            for (int i = 0; i < _itemHolder.childCount || i < items.Count; i++)
            {
                if (i < _itemHolder.childCount)
                {
                    _itemHolder.GetChild(i).GetComponent<ItemSlot>().Init(items[i]);
                }
                else
                {
                    Instantiate(_itemSlotPrefab, _itemHolder).Init(items[i]);
                }
            }

            // set my potions
            for (int i = 0; i < _potionHolder.childCount || i < player.Potions.Count; i++)
            {
                if (i < _potionHolder.childCount)
                {
                    _potionHolder.GetChild(i).GetComponent<PotionSlot>().Init(player.Potions[i]);
                    _potionHolder.GetChild(i).gameObject.SetActive(true);
                }
                else if (i >= player.Potions.Count)
                {
                    _potionHolder.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    Instantiate(_potionSlotPrefab, _potionHolder).Init(player.Potions[i]);
                }
            }
        }
    }

    private void OnClickTabButton(int index)
    {
        _rewardHolder.SetActive(index == 0);
        _myInfoHolder.SetActive(index == 1);
    }

    private void OnClickSubTabButton(int index)
    {
        _statHolder.SetActive(index == 0);
        _itemHolder.gameObject.SetActive(index == 1);
        _skillHolder.gameObject.SetActive(index == 2);
        _potionHolder.gameObject.SetActive(index == 3);
    }
}
