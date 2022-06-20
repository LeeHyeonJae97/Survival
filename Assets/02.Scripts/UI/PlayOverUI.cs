using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayOverUI : UI
{
    [SerializeField] private string _title;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameObject _timeGroup;
    [SerializeField] private RectTransform[] _elapsedTextHolders;
    [SerializeField] private RectTransform[] _myTimeTextHolders;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private PlayOverSlot _characterSlot;
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private Transform _itemSlotHolder;
    [SerializeField] private TextMeshProUGUI _skillText;
    [SerializeField] private Transform _skillSlotHolder;
    [SerializeField] private PlayOverSlot _playOverSlotPrefab;
    [SerializeField] private GameObject _guideText;

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
        channel.OnPlayFinished -= OnPlayFinished;
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            StartCoroutine(CoShow());
        }
    }

    private IEnumerator CoShow()
    {
        // hide all
        _button.interactable = false;
        _timeGroup.SetActive(false);
        _characterText.gameObject.SetActive(false);
        _characterSlot.gameObject.SetActive(false);
        _itemText.gameObject.SetActive(false);
        _skillText.gameObject.SetActive(false);
        _guideText.SetActive(false);

        // title
        yield return StartCoroutine(CoTextAnimation(_titleText, _titleText.text));

        // time
        StartCoroutine(CoCalculateElapsed());
        // save gained time
        GameManager.GetInstance().User.Time += PlayManager.GetInstance().ElapsedSec;

        // character info
        yield return StartCoroutine(CoTextAnimation(_characterText, _characterText.text));
        _characterSlot.Init(Player.GetInstance().Character.Info.Sprite);
        _characterSlot.gameObject.SetActive(true);

        // item info
        yield return StartCoroutine(CoTextAnimation(_itemText, _itemText.text));
        foreach (var item in Player.GetInstance().ItemDic.Values)
        {
            Instantiate(_playOverSlotPrefab, _itemSlotHolder).Init(item.Item.Info.Icon);
        }

        // skill info
        yield return StartCoroutine(CoTextAnimation(_skillText, _skillText.text));
        foreach (var skill in Player.GetInstance().SkillDic.Values)
        {
            Instantiate(_playOverSlotPrefab, _skillSlotHolder).Init(skill.Skill.Info.Icon);
        }

        // set interactable button
        _button.interactable = true;
        _guideText.SetActive(true);
    }

    private void OnPlayFinished()
    {
        SetActive(true);
    }

    private void OnClickButton()
    {
        _button.interactable = false;

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

    private IEnumerator CoTextAnimation(TextMeshProUGUI text, string content)
    {
        text.text = "";
        text.gameObject.SetActive(true);

        for (int i = 0; i < content.Length; i++)
        {
            text.text += content[i];
            yield return WaitForSecondsFactory.Get(0.15f);
        }
    }

    private IEnumerator CoCalculateElapsed()
    {
        int childCount = _elapsedTextHolders[0].childCount;
        float height = ((RectTransform)_elapsedTextHolders[0].GetChild(0)).sizeDelta.y;
        float top = (childCount - 1) * height;
        float speed = 120;

        // set elapsed
        int elapsedSec = PlayManager.GetInstance().ElapsedSec;
        string elapsed = $"{elapsedSec / 60:00}{elapsedSec % 60:00}";

        for (int i = 0; i < elapsed.Length; i++)
        {
            _elapsedTextHolders[i].anchoredPosition
                = new Vector2(_elapsedTextHolders[i].anchoredPosition.x, (9 - (elapsed[i] - '0')) * height);
        }

        // set my time
        int myTimeSec = GameManager.GetInstance().User.Time;
        string myTime = $"{myTimeSec / 60:00}{myTimeSec % 60:00}";

        for (int i = 0; i < myTime.Length; i++)
        {
            _myTimeTextHolders[i].anchoredPosition
                = new Vector2(_myTimeTextHolders[i].anchoredPosition.x, (9 - (myTime[i] - '0')) * height);
        }

        // set active
        _timeGroup.SetActive(true);

        // animation for elapsed
        StartCoroutine(CoElapsedAnimation(elapsedSec, height, speed, top));

        // animation for my time
        yield return WaitForSecondsFactory.Get(0.25f);
        StartCoroutine(CoMyTimeAnimation(elapsedSec, height, speed, top));
    }

    private IEnumerator CoElapsedAnimation(int elapsedSec, float height, float speed, float top)
    {
        for (int i = 0; i < elapsedSec; i++)
        {
            for (int j = _elapsedTextHolders.Length - 1; j >= 0; j--)
            {
                if (Mathf.Approximately(_elapsedTextHolders[j].anchoredPosition.y, top - height))
                {
                    StartCoroutine(CoElapsedAnimationInternal(j, height, speed, top));
                }
                else
                {
                    yield return StartCoroutine(CoElapsedAnimationInternal(j, height, speed, top));
                    break;
                }
            }
        }
    }

    private IEnumerator CoMyTimeAnimation(int elapsedSec, float height, float speed, float top)
    {
        for (int i = 0; i < elapsedSec; i++)
        {
            for (int j = _myTimeTextHolders.Length - 1; j >= 0; j--)
            {
                if (Mathf.Approximately(_myTimeTextHolders[j].anchoredPosition.y, height))
                {
                    StartCoroutine(CoMyTimeAnimationInternal(j, height, speed, top));
                }
                else
                {
                    yield return StartCoroutine(CoMyTimeAnimationInternal(j, height, speed, top));
                    break;
                }
            }
        }
    }

    private IEnumerator CoElapsedAnimationInternal(int index, float height, float speed, float top)
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

    private IEnumerator CoMyTimeAnimationInternal(int index, float height, float speed, float top)
    {
        Vector2 target = new Vector2(_myTimeTextHolders[index].anchoredPosition.x, _myTimeTextHolders[index].anchoredPosition.y - height);

        while (!Mathf.Approximately(_myTimeTextHolders[index].anchoredPosition.y, target.y))
        {
            _myTimeTextHolders[index].anchoredPosition = Vector2.MoveTowards(_myTimeTextHolders[index].anchoredPosition, target, speed * Time.unscaledDeltaTime);
            yield return null;
        }

        if (Mathf.Approximately(target.y, 0))
        {
            _myTimeTextHolders[index].anchoredPosition = new Vector2(_myTimeTextHolders[index].anchoredPosition.x, top);
        }
    }
}
