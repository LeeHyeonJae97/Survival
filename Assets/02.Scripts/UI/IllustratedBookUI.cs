using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookUI : UI
{
    [SerializeField] private IllustratedBookSlot _slotPrefab;
    [SerializeField] private Transform _itemHolder;
    [SerializeField] private Transform _skillHolder;
    [SerializeField] private Transform _potionHolder;
    [SerializeField] private Transform _enemyHolder;
    [SerializeField] private Button _itemTabButton;
    [SerializeField] private Button _skillTabButton;
    [SerializeField] private Button _potionTabButton;
    [SerializeField] private Button _enemyTabButton;
    [SerializeField] private Button _backButton;

    protected override void Awake()
    {
        base.Awake();

        IllustratedBook ib = GameManager.Instance.User.IllustratedBook;

        // instantiate item illustrated book slots
        for (int i = 0; i < ItemFactory.Count; i++) Instantiate(_slotPrefab, _itemHolder);

        // instantiate skill illustrated book slots
        for (int i = 0; i < SkillFactory.Count; i++) Instantiate(_slotPrefab, _skillHolder);

        // instantiate skill illustrated book slots
        for (int i = 0; i < PotionFactory.Count; i++) Instantiate(_slotPrefab, _potionHolder);

        // instantiate enemy illustrated book slots
        for (int i = 0; i < EnemyFactory.Count; i++) Instantiate(_slotPrefab, _enemyHolder);

        _itemTabButton.onClick.AddListener(() => OnClickTabButton(0));
        _skillTabButton.onClick.AddListener(() => OnClickTabButton(1));
        _potionTabButton.onClick.AddListener(() => OnClickTabButton(2));
        _enemyTabButton.onClick.AddListener(() => OnClickTabButton(3));

        _backButton.onClick.AddListener(OnClickBackButton);
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            IllustratedBook ib = GameManager.Instance.User.IllustratedBook;

            // initialize item illustrated book slots
            for (int i = 0; i < _itemHolder.childCount; i++)
            {
                _itemHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(ItemFactory.Get(i), ib.Items[i]);
            }

            // initialize skill illustrated book slots
            for (int i = 0; i < _skillHolder.childCount; i++)
            {
                _skillHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(SkillFactory.Get(i), ib.Skills[i]);
            }

            // initialize skill illustrated book slots
            for (int i = 0; i < _potionHolder.childCount; i++)
            {
                _potionHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(PotionFactory.Get(i), ib.Skills[i]);
            }

            // initialize enemy illustrated book slots
            for (int i = 0; i < _enemyHolder.childCount; i++)
            {
                _enemyHolder.GetChild(i).GetComponent<IllustratedBookSlot>().Init(EnemyFactory.Get(i), ib.Enemies[i]);
            }

            // select item tab
            OnClickTabButton(0);
        }
    }

    private void OnClickTabButton(int index)
    {
        _itemHolder.gameObject.SetActive(index == 0);
        _skillHolder.gameObject.SetActive(index == 1);
        _potionHolder.gameObject.SetActive(index == 2);
        _enemyHolder.gameObject.SetActive(index == 3);

        _itemTabButton.targetGraphic.color = index == 0 ? _itemTabButton.colors.normalColor : _itemTabButton.colors.disabledColor;
        _skillTabButton.targetGraphic.color = index == 1 ? _skillTabButton.colors.normalColor : _skillTabButton.colors.disabledColor;
        _potionTabButton.targetGraphic.color = index == 2 ? _potionTabButton.colors.normalColor : _potionTabButton.colors.disabledColor;
        _enemyTabButton.targetGraphic.color = index == 3 ? _enemyTabButton.colors.normalColor : _enemyTabButton.colors.disabledColor;
    }

    private void OnClickBackButton()
    {
        SetActive(false);
    }
}