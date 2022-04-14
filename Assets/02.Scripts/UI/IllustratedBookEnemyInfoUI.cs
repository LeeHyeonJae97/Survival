using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllustratedBookEnemyInfoUI : UI
{
    [SerializeField] private Image _enemyImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI[] _statTexts;
    [SerializeField] private Button _coverButton;

    protected override void Awake()
    {
        base.Awake();

        _coverButton.onClick.AddListener(OnClickCoverButton);
    }

    public void SetActive(bool value, Enemy enemy)
    {
        if (value)
        {
            _enemyImage.sprite = enemy.Info.Sprite;

            _nameText.text = enemy.Info.Name;

            _descriptionText.text = enemy.Info.Description;

            for (int i = 0; i < _statTexts.Length; i++)
            {
                _statTexts[i].text = $"{enemy.Info.Stats[i].Value}";
            }
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
