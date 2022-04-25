using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : UI
{
    [SerializeField] private TextMeshProUGUI _levelExpText;
    [SerializeField] private Button _storyModeButton;
    [SerializeField] private Button _survivlaModeButton;
    [SerializeField] private Button _illustratedBookButton;

    protected override void Awake()
    {
        base.Awake();

        _storyModeButton.onClick.AddListener(() => OnClickPlayButton(PlayMode.Story));
        _survivlaModeButton.onClick.AddListener(() => OnClickPlayButton(PlayMode.Survival));
        _illustratedBookButton.onClick.AddListener(OnClickIllustratedBookButton);
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            var user = GameManager.Instance.User;
            _levelExpText.text = $"수준 {user.Level} 경험치 {user.Exp} / {user.MaxExp}";
        }
    }

    private void OnClickPlayButton(PlayMode playMode)
    {
        // set mode
        PlayManager.PlayMode = playMode;

        // loading
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();

        loadingUI.Title = "떠날 채비를 하는 중입니다.";
        loadingUI.onSetActive += OnLoadingCompleted;
        loadingUI.SetActive(true);
    }

    private void OnClickIllustratedBookButton()
    {
        UIFactory.Get<IllustratedBookUI>().SetActive(true);
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
                op2.completed += (op) => UIFactory.Get<LoadingUI>().SetActive(false);
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