using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : UI
{
    [SerializeField] private Button _storyModeButton;
    [SerializeField] private Button _survivalModeButton;
    [SerializeField] private Button _illustratedBookButton;
    [SerializeField] private Button _exitButton;

    protected override void Awake()
    {
        base.Awake();

        _storyModeButton.onClick.AddListener(OnClickStoryModeButton);
        _survivalModeButton.onClick.AddListener(OnClickSurvivalModeButton);
        _illustratedBookButton.onClick.AddListener(OnClickIllustratedBookButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override void OnSetActive(bool value)
    {

    }

    private void OnClickStoryModeButton()
    {
        // set mode
        PlayManager.PlayMode = PlayMode.Story;

        // loading
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();

        loadingUI.Title = "떠날 채비를 하는 중입니다.";
        loadingUI.onSetActive += (value) =>
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
                    op2.completed += (op) => loadingUI.SetActive(false);
                };
            }
            else
            {
                // after all loading is done, invoke play started event
                EventChannelFactory.Get<PlayEventChannelSO>().OnPlayStarted();
            }
        };
        loadingUI.SetActive(true);
    }

    private void OnClickSurvivalModeButton()
    {
        // set mode
        PlayManager.PlayMode = PlayMode.Survival;

        // loading
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();

        loadingUI.Title = "떠날 채비를 하는 중입니다.";
        loadingUI.onSetActive += (value) =>
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
                    op2.completed += (op) => loadingUI.SetActive(false);
                };
            }
            else
            {
                // after all loading is done, invoke play started event
                EventChannelFactory.Get<PlayEventChannelSO>().OnPlayStarted();
            }
        };
        loadingUI.SetActive(true);
    }

    private void OnClickIllustratedBookButton()
    {
        UIFactory.Get<IllustratedBookUI>().SetActive(true);
    }

    private void OnClickExitButton()
    {
        // confirm if want to quit
        UIFactory.Get<ConfirmUI>().Confirm("정말 종료하시겠습니까?", () => Application.Quit());
    }
}