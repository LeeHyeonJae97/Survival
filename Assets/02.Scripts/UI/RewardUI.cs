using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class RewardUI : UI
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Button _nextWaveButton;
    [SerializeField] private Button _itemTabButton;
    [SerializeField] private Button _skillTabButton;
    [SerializeField] private Button _potionTabButton;
    [SerializeField] private Button _gamblingTabButton;
    [SerializeField] private Button _myInfoTabButton;
    [SerializeField] private Transform _itemGroup;
    [SerializeField] private Transform _skillGroup;
    [SerializeField] private Transform _potionGroup;
    [SerializeField] private GameObject _gamblingGroup;
    [SerializeField] private GameObject _myInfoGroup;
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
    [SerializeField] private Button _myStatTabButton;
    [SerializeField] private Button _myItemTabButton;
    [SerializeField] private Button _mySkillTabButton;
    [SerializeField] private Button _myPotionTabButton;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private TMP_InputField _coinInputField;
    [SerializeField] private Button[] _keyButtons;
    [SerializeField] private Button _backspaceButton;
    [SerializeField] private Button _enterButton;
    [SerializeField] private RectTransform _slotContent;

    protected override void Awake()
    {
        base.Awake();

        _nextWaveButton.onClick.AddListener(OnClickNextWaveButton);

        _itemTabButton.onClick.AddListener(() => OnClickTabButton(0));
        _skillTabButton.onClick.AddListener(() => OnClickTabButton(1));
        _potionTabButton.onClick.AddListener(() => OnClickTabButton(2));
        _gamblingTabButton.onClick.AddListener(() => OnClickTabButton(3));
        _myInfoTabButton.onClick.AddListener(() => OnClickTabButton(4));

        _myStatTabButton.onClick.AddListener(() => OnClickSubTabButton(0));
        _myItemTabButton.onClick.AddListener(() => OnClickSubTabButton(1));
        _mySkillTabButton.onClick.AddListener(() => OnClickSubTabButton(2));
        _myPotionTabButton.onClick.AddListener(() => OnClickSubTabButton(3));

        for (int i = 0; i < _keyButtons.Length; i++)
        {
            int index = i;
            _keyButtons[index].onClick.AddListener(() => OnClickKeyButton(index));
        }
        _backspaceButton.onClick.AddListener(OnClickBackspaceButton);
        _enterButton.onClick.AddListener(OnClickEnterButton);

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnWaveStarted += OnWaveStarted;
        channel.OnWaveFinished += OnWaveFinished;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnWaveStarted -= OnWaveStarted;
        channel.OnWaveFinished -= OnWaveFinished;
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            Init();

            OnClickTabButton(0);
            OnClickSubTabButton(0);

            OnCoinUpdated(Player.GetInstance().Coin);
            Player.GetInstance().onCoinUpdated += OnCoinUpdated;
        }
        else
        {
            Player.GetInstance().onCoinUpdated -= OnCoinUpdated;
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

    private void Init()
    {
        Item[] items = ItemFactory.GetRandom(Reward.Infos[(int)RewardType.Item].Count);

        for (int i = 0; i < _itemGroup.childCount && i < items.Length; i++)
        {
            _itemGroup.GetChild(i).GetComponent<ItemSlot>().Init(items[i]);
        }

        Skill[] skills = SkillFactory.GetRandom(Reward.Infos[(int)RewardType.Skill].Count);

        for (int i = 0; i < _skillGroup.childCount && i < skills.Length; i++)
        {
            _skillGroup.GetChild(i).GetComponent<SkillSlot>().Init(skills[i]);
        }

        Potion[] potions = PotionFactory.GetRandom(Reward.Infos[(int)RewardType.Potion].Count);

        for (int i = 0; i < _potionGroup.childCount && i < potions.Length; i++)
        {
            _potionGroup.GetChild(i).GetComponent<PotionSlot>().Init(potions[i]);
        }
    }

    private void Init(Player player)
    {
        // set character image
        _characterImage.sprite = player.Character.Info.Sprite;

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

    private void OnCoinUpdated(int coin)
    {
        _coinText.text = $"{coin}";
    }

    private void OnClickNextWaveButton()
    {
        UIFactory.Get<ConfirmUI>().Confirm("확실합니까?", EventChannelFactory.Get<PlayEventChannelSO>().StartWave);
    }

    private void OnClickTabButton(int index)
    {
        _itemGroup.gameObject.SetActive(index == 0);
        _skillGroup.gameObject.SetActive(index == 1);
        _potionGroup.gameObject.SetActive(index == 2);
        _gamblingGroup.SetActive(index == 3);
        _myInfoGroup.SetActive(index == 4);

        _itemTabButton.targetGraphic.color = index == 0 ? _itemTabButton.colors.normalColor : _itemTabButton.colors.disabledColor;
        _skillTabButton.targetGraphic.color = index == 1 ? _skillTabButton.colors.normalColor : _skillTabButton.colors.disabledColor;
        _potionTabButton.targetGraphic.color = index == 2 ? _potionTabButton.colors.normalColor : _potionTabButton.colors.disabledColor;
        _gamblingTabButton.targetGraphic.color = index == 3 ? _gamblingTabButton.colors.normalColor : _gamblingTabButton.colors.disabledColor;
        _myInfoTabButton.targetGraphic.color = index == 4 ? _myInfoTabButton.colors.normalColor : _myInfoTabButton.colors.disabledColor;

        if (index == 3)
        {
            float top = (_slotContent.childCount - 1) * ((RectTransform)_slotContent.GetChild(0)).sizeDelta.y;
            _slotContent.anchoredPosition = new Vector2(_slotContent.anchoredPosition.x, top);
        }
        else if (index == 4)
        {
            Init(Player.GetInstance());
        }
    }

    private void OnClickSubTabButton(int index)
    {
        _myStatGroup.SetActive(index == 0);
        _myItemGroup.gameObject.SetActive(index == 1);
        _mySkillGroup.gameObject.SetActive(index == 2);
        _myPotionGroup.gameObject.SetActive(index == 3);

        _myStatTabButton.targetGraphic.color = index == 0 ? _myStatTabButton.colors.normalColor : _myStatTabButton.colors.disabledColor;
        _myItemTabButton.targetGraphic.color = index == 1 ? _myItemTabButton.colors.normalColor : _myItemTabButton.colors.disabledColor;
        _mySkillTabButton.targetGraphic.color = index == 2 ? _mySkillTabButton.colors.normalColor : _mySkillTabButton.colors.disabledColor;
        _myPotionTabButton.targetGraphic.color = index == 3 ? _myPotionTabButton.colors.normalColor : _myPotionTabButton.colors.disabledColor;
    }

    private void OnClickKeyButton(int value)
    {
        _coinInputField.text += $"{value + 1}";
    }

    private void OnClickBackspaceButton()
    {
        if (_coinInputField.text.Length > 0)
        {
            _coinInputField.text = _coinInputField.text.Remove(_coinInputField.text.Length - 1);
        }
    }

    private void OnClickEnterButton()
    {
        if (string.IsNullOrEmpty(_coinInputField.text)) return;

        // start gambling
        StartCoroutine(CoSlot());
    }

    private IEnumerator CoSlot()
    {
        int minRound = 5;
        int maxRound = 13;
        int slowThreshold = -5;
        float fastSpeed = 200;
        float slowSpeed = 100;

        int childCount = _slotContent.childCount - 1;
        int count = childCount * Random.Range(minRound, maxRound) + Random.Range(0, childCount);
        float height = ((RectTransform)_slotContent.GetChild(0)).sizeDelta.y;
        float top = childCount * height;

        _slotContent.anchoredPosition = new Vector2(_slotContent.anchoredPosition.x, top);

        for (int i = 0; i < count; i++)
        {
            Vector2 target = new Vector2(_slotContent.anchoredPosition.x, _slotContent.anchoredPosition.y - height);

            while (!Mathf.Approximately(_slotContent.anchoredPosition.y, target.y))
            {
                _slotContent.anchoredPosition = Vector2.MoveTowards(_slotContent.anchoredPosition, target,
                    (i < count + slowThreshold ? fastSpeed : slowSpeed) * Time.unscaledDeltaTime);
                yield return null;
            }

            if (Mathf.Approximately(target.y, 0)) _slotContent.anchoredPosition = new Vector2(_slotContent.anchoredPosition.x, top);
        }
    }
}
