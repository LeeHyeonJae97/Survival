using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedUI : UI
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _toTitleButton;

    protected override void Awake()
    {
        base.Awake();

        _resumeButton.onClick.AddListener(OnClickResumeButton);
        _toTitleButton.onClick.AddListener(OnClickToTitleButton);

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPaused += OnPaused;
        channel.OnResumed += OnResumed;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPaused -= OnPaused;
        channel.OnResumed -= OnResumed;
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnPaused()
    {
        SetActive(true);
    }

    private void OnResumed()
    {
        SetActive(false);
    }

    private void OnClickResumeButton()
    {
        EventChannelFactory.Get<PlayEventChannelSO>().Resume();
    }

    private void OnClickToTitleButton()
    {
        // loading
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();

        loadingUI.Title = "추격을 따돌리는 중입니다.";
        loadingUI.onSetActive += OnLoadingCompleted;
        loadingUI.SetActive(true);
    }

    private void OnLoadingCompleted(bool value)
    {
        if (value)
        {
            // unload play scene
            var op1 = SceneManager.UnloadSceneAsync("Play");
            op1.completed += (op) =>
            {
                // load title scene
                var op2 = SceneManager.LoadSceneAsync("Title", LoadSceneMode.Additive);
                // after title scene is loaded, inactivate loading ui
                op2.completed += (op) =>
                {
                    LoadingUI loadingUI = UIFactory.Get<LoadingUI>();
                    loadingUI.SetActive(false);
                    loadingUI.onSetActive -= OnLoadingCompleted;
                };
            };
        }
    }
}
