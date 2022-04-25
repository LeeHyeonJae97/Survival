using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveInfoSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _rewardTypeImage;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private Image[] _enemyImages;
    [SerializeField] private Button _button;

    // TODO :
    // implement for story mode

    public void Init(string name, RewardType rewardType, float duration, float interval, Enemy[] enemies)
    {
        _nameText.text = name;
        _rewardTypeImage.sprite = Reward.Infos[(int)rewardType].Icon;
        _durationText.text = $"{duration}";
        for (int i = 0; i < _enemyImages.Length; i++)
        {
            if (i < enemies.Length)
            {
                _enemyImages[i].sprite = enemies[i].Info.Sprite;
                _enemyImages[i].gameObject.SetActive(true);
            }
            else
            {
                _enemyImages[i].gameObject.SetActive(false);
            }
        }

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            UIFactory.Get<ConfirmUI>().Confirm("확실합니까?", () =>
            {
                // update ui
                UIFactory.Get<NextWaveSelectionUI>().SetActive(false);

                // move on to the next wave
                EventChannelFactory.Get<PlayEventChannelSO>().StartWave();
            });
        });
    }
}
