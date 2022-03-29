using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : UI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    protected override void Awake()
    {
        base.Awake();

        _playButton.onClick.AddListener(OnClickPlayButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnClickPlayButton()
    {
        StartCoroutine(CoOnClickPlayButton());
    }

    private IEnumerator CoOnClickPlayButton()
    {
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();
        loadingUI.onSetActive += (value) =>
        {
            if (!value) EventChannelFactory.Get<PlayEventChannelSO>().OnPlayStarted();
        };

        loadingUI.SetActive(true);
        var operation = SceneManager.LoadSceneAsync("Play");
        yield return new WaitUntil(() => loadingUI.IsActive && operation.isDone);

        loadingUI.SetActive(false);
    }

    private void OnClickExitButton()
    {
        UIFactory.Get<ConfirmUI>().Confirm("really want to quit?", () => Application.Quit());
    }
}