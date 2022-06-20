using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : UI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _illustratedBookButton;

    protected override void Awake()
    {
        base.Awake();

        _playButton.onClick.AddListener(OnClickPlayButton);
        _illustratedBookButton.onClick.AddListener(OnClickIllustratedBookButton);
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            var user = GameManager.GetInstance().User;

            // TODO :
            // show user's time
            //
            //_levelExpText.text = $"수준 {user.Level} 경험치 {user.Exp} / {user.MaxExp}";
        }
    }

    private void OnClickPlayButton()
    {
        UIFactory.Get<StageSelectionUI>().SetActive(true);
    }

    private void OnClickIllustratedBookButton()
    {
        UIFactory.Get<IllustratedBookUI>().SetActive(true);
    }
}