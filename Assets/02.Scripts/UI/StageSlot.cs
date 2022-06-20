using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    [SerializeField] private Button _slotButton;
    [SerializeField] private TextMeshProUGUI _nameText;
    //[SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveNameText;
    [SerializeField] private TextMeshProUGUI _waveDescriptionText;
    [SerializeField] private TextMeshProUGUI _waveDescriptionIndexText;
    [SerializeField] private Button _descriptionLeftButton;
    [SerializeField] private Button _descriptionRightButton;
    [SerializeField] private Button _startButton;
    private Stage _stage;
    private int _currentWaveDescriptionIndex;

    public void Init(Stage stage, UnityAction<StageSlot> onClickSlotButton)
    {
        _nameText.text = stage.Info.Name;
        //_waveText.text = $"총 {stage.Info.Waves.Length}번의 흐름";

        if (_stage == null)
        {
            _slotButton.onClick.AddListener(() => onClickSlotButton(this));
            _startButton.onClick.AddListener(OnClickPlayButton);
            _descriptionLeftButton.onClick.AddListener(() => OnClickDescriptionTurnButton(true));
            _descriptionRightButton.onClick.AddListener(() => OnClickDescriptionTurnButton(false));
        }

        _stage = stage;

        SetActiveDetail(false);
    }

    public void OnClickSlotButton()
    {
        SetActiveDetail(!_waveNameText.gameObject.activeInHierarchy);
    }

    private void SetActiveDetail(bool active)
    {
        // set active detail ui
        _currentWaveDescriptionIndex = 0;
        SetDetail();
        _waveNameText.gameObject.SetActive(active);
        _waveDescriptionText.gameObject.SetActive(active);
        _waveDescriptionIndexText.gameObject.SetActive(active);
        _startButton.gameObject.SetActive(active);

        UpdateLayout();
    }

    private void OnClickDescriptionTurnButton(bool left)
    {
        if (left)
        {
            _currentWaveDescriptionIndex = Mathf.Max(_currentWaveDescriptionIndex - 1, 0);
        }
        else
        {
            _currentWaveDescriptionIndex = Mathf.Min(_currentWaveDescriptionIndex + 1, _stage.Info.Waves.Length - 1);
        }
        SetDetail();
        UpdateLayout();
    }

    private void OnClickPlayButton()
    {
        // loading
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();

        loadingUI.Title = "떠날 채비를 하는 중입니다.";
        loadingUI.onSetActive += OnLoadingCompleted;
        loadingUI.SetActive(true);
    }

    private void SetDetail()
    {
        string name = _stage.Info.Waves[_currentWaveDescriptionIndex].Name;
        string description = _stage.Info.Waves[_currentWaveDescriptionIndex].Description;
        if (_currentWaveDescriptionIndex >= _stage.ClearedIndex)
        {
            char[] exceptChars = { '\n', ' ', '.', ',' };
            name = name.ReplaceExcept(exceptChars, '?');
            description = description.ReplaceExcept(exceptChars, '?');
        }
        _waveNameText.text = name;
        _waveDescriptionText.text = description;
        _waveDescriptionIndexText.text = $"{_currentWaveDescriptionIndex + 1} / {_stage.Info.Waves.Length}";
    }

    private void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent);
    }

    private void OnLoadingCompleted(bool value)
    {
        if (value)
        {
            // unload title scene
            var op1 = SceneManager.UnloadSceneAsync("Title");
            op1.completed += (op) =>
            {
                // load play scene
                var op2 = SceneManager.LoadSceneAsync("Play", LoadSceneMode.Additive);
                // after play scene is loaded, inactivate loading ui
                op2.completed += (op) =>
                {
                    WaveManager.GetInstance().StageId = _stage.Info.Id;
                    UIFactory.Get<LoadingUI>().SetActive(false);
                };
            };
        }
        else
        {
            // remove callback
            UIFactory.Get<LoadingUI>().onSetActive -= OnLoadingCompleted;

            // after all loading is done, invoke play started event
            EventChannelFactory.Get<PlayEventChannelSO>().StartPlay();
        }
    }
}
