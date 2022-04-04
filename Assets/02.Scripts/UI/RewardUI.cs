using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UI
{
    [SerializeField] private Button _rewardTabButton;
    [SerializeField] private Button _myInfoTabButton;
    [SerializeField] private GameObject _rewardGroup;
    [SerializeField] private GameObject _myInfoGroup;
    [SerializeField] private Transform _rewardItemGroup;
    [SerializeField] private Transform _rewardSkillGroup;
    [SerializeField] private Transform _rewardPotionGroup;
    [SerializeField] private Image _characterImage;
    [SerializeField] private Image[] _statImages;
    [SerializeField] private TextMeshProUGUI[] _statTexts;
    [SerializeField] private GameObject _myStatGroup;
    [SerializeField] private GameObject _myItemGroup;
    [SerializeField] private GameObject _mySkillGroup;
    [SerializeField] private GameObject _myPotionGroup;
    [SerializeField] private Transform _itemSlotHolder;
    [SerializeField] private Transform _skillSlotHolder;
    [SerializeField] private Transform _potionSlotHolder;
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
        _potionTabButton.onClick.AddListener(() => OnClickSubTabButton(3));

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.onWaveStarted += OnWaveStarted;
        channel.onWaveFinished += OnWaveFinished;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.onWaveStarted -= OnWaveStarted;
        channel.onWaveFinished -= OnWaveFinished;
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            Init(WaveManager.Instance.Wave.RewardType);
            Init(Player.Instance);

            OnClickTabButton(0);
            OnClickSubTabButton(0);
        }
    }

    private void OnWaveStarted()
    {
        SetActive(false);
    }

    private void OnWaveFinished()
    {
        SetActive(true);
    }

    private void Init(RewardType type)
    {
        switch (type)
        {
            case RewardType.Item:
                ItemSO[] items = ItemFactory.GetRandom(Reward.Infos[(int)RewardType.Item].Count);
                for (int i = 0; i < _rewardItemGroup.childCount; i++)
                {
                    _rewardItemGroup.GetChild(i).GetComponent<ItemSlot>().Init(items[i]);
                }
                _rewardItemGroup.gameObject.SetActive(true);
                break;

            case RewardType.Skill:
                SkillSO[] skills = SkillFactory.GetRandom(Reward.Infos[(int)RewardType.Skill].Count);
                for (int i = 0; i < _rewardSkillGroup.childCount; i++)
                {
                    _rewardSkillGroup.GetChild(i).GetComponent<SkillSlot>().Init(skills[i]);
                }
                _rewardSkillGroup.gameObject.SetActive(true);
                break;

            case RewardType.Potion:
                PotionSO[] potions = PotionFactory.GetRandom(Reward.Infos[(int)RewardType.Potion].Count);
                for (int i = 0; i < _rewardPotionGroup.childCount; i++)
                {
                    _rewardPotionGroup.GetChild(i).GetComponent<PotionSlot>().Init(potions[i]);
                }
                _rewardPotionGroup.gameObject.SetActive(true);
                break;
        }
    }

    private void Init(Player player)
    {
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

        for (int i = 0; i < _skillSlotHolder.childCount || i < skills.Count; i++)
        {
            if (i < _skillSlotHolder.childCount)
            {
                _skillSlotHolder.GetChild(i).GetComponent<SkillSlot>().Init(skills[i]);
            }
            else
            {
                Instantiate(_skillSlotPrefab, _skillSlotHolder).Init(skills[i]);
            }
        }

        // set my items
        List<LiveItem> items = new List<LiveItem>(player.ItemDic.Values);

        for (int i = 0; i < _itemSlotHolder.childCount || i < items.Count; i++)
        {
            if (i < _itemSlotHolder.childCount)
            {
                _itemSlotHolder.GetChild(i).GetComponent<ItemSlot>().Init(items[i]);
            }
            else
            {
                Instantiate(_itemSlotPrefab, _itemSlotHolder).Init(items[i]);
            }
        }

        // set my potions
        for (int i = 0; i < _potionSlotHolder.childCount || i < player.Potions.Count; i++)
        {
            if (i < _potionSlotHolder.childCount)
            {
                _potionSlotHolder.GetChild(i).GetComponent<PotionSlot>().Init(player.Potions[i]);
                _potionSlotHolder.GetChild(i).gameObject.SetActive(true);
            }
            else if (i >= player.Potions.Count)
            {
                _potionSlotHolder.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                Instantiate(_potionSlotPrefab, _potionSlotHolder).Init(player.Potions[i]);
            }
        }
    }

    private void OnClickTabButton(int index)
    {
        _rewardGroup.SetActive(index == 0);
        _myInfoGroup.SetActive(index == 1);

        _rewardTabButton.targetGraphic.color = index == 0 ? _rewardTabButton.colors.normalColor : _rewardTabButton.colors.disabledColor;
        _myInfoTabButton.targetGraphic.color = index == 1 ? _myInfoTabButton.colors.normalColor : _myInfoTabButton.colors.disabledColor;
    }

    private void OnClickSubTabButton(int index)
    {
        _myStatGroup.SetActive(index == 0);
        _myItemGroup.gameObject.SetActive(index == 1);
        _mySkillGroup.gameObject.SetActive(index == 2);
        _myPotionGroup.gameObject.SetActive(index == 3);

        _statTabButton.targetGraphic.color = index == 0 ? _statTabButton.colors.normalColor : _statTabButton.colors.disabledColor;
        _itemTabButton.targetGraphic.color = index == 1 ? _itemTabButton.colors.normalColor : _itemTabButton.colors.disabledColor;
        _skillTabButton.targetGraphic.color = index == 2 ? _skillTabButton.colors.normalColor : _skillTabButton.colors.disabledColor;
        _potionTabButton.targetGraphic.color = index == 3 ? _potionTabButton.colors.normalColor : _potionTabButton.colors.disabledColor;
    }
}
