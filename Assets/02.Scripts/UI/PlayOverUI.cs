using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayOverUI : UI
{
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform[] _elapsedTextHolders;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private RectTransform[] _expTextHolders;
    [SerializeField] private TextMeshProUGUI _maxExpText;

    protected override void Awake()
    {
        base.Awake();

        _button.onClick.AddListener(OnClickButton);

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayFinished += OnPlayFinished;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        var channel = EventChannelFactory.Get<PlayEventChannelSO>();
        channel.OnPlayFinished += OnPlayFinished;
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            StartCoroutine(CoSlot());
        }
    }

    private void OnPlayFinished()
    {
        SetActive(true);
    }

    private void OnClickButton()
    {
        // loading
        LoadingUI loadingUI = UIFactory.Get<LoadingUI>();

        loadingUI.Title = "삼보 전진을 위해 일보 후퇴하는 중입니다.";
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

    private IEnumerator CoSlot()
    {
        int elapsedSec = PlayManager.Instance.ElapsedSec;
        int childCount = _elapsedTextHolders[0].childCount;
        float height = ((RectTransform)_elapsedTextHolders[0].GetChild(0)).sizeDelta.y;
        float top = (childCount - 1) * height;
        float speed = 60;

        string elapsed = $"{elapsedSec / 60:00}{elapsedSec % 60:00}";

        for (int i = 0; i < elapsed.Length; i++)
        {
            _elapsedTextHolders[i].anchoredPosition = new Vector2(_elapsedTextHolders[i].anchoredPosition.x, (9 - (elapsed[i] - '0')) * height);
        }

        // delay
        yield return WaitForSecondsFactory.Get(0.5f);

        // animation
        for (int i = 0; i < elapsedSec; i++)
        {
            for (int j = _elapsedTextHolders.Length - 1; j >= 0; j--)
            {
                if (Mathf.Approximately(_elapsedTextHolders[j].anchoredPosition.y, top - height))
                {
                    StartCoroutine(CoSlotInternal(j, height, speed, top));
                }
                else
                {
                    yield return StartCoroutine(CoSlotInternal(j, height, speed, top));
                    break;
                }
            }
        }
    }

    private IEnumerator CoSlotInternal(int index, float height, float speed, float top)
    {
        Vector2 target = new Vector2(_elapsedTextHolders[index].anchoredPosition.x, _elapsedTextHolders[index].anchoredPosition.y + height);

        while (!Mathf.Approximately(_elapsedTextHolders[index].anchoredPosition.y, target.y))
        {
            _elapsedTextHolders[index].anchoredPosition = Vector2.MoveTowards(_elapsedTextHolders[index].anchoredPosition, target, speed * Time.unscaledDeltaTime);
            yield return null;
        }

        if (Mathf.Approximately(target.y, top))
        {
            _elapsedTextHolders[index].anchoredPosition = new Vector2(_elapsedTextHolders[index].anchoredPosition.x, 0);
        }
    }
}
