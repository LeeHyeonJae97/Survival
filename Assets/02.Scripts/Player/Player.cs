using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : SingletonMonoBehaviour<Player>, IDamageable
{
    // TODO :
    // need to be static
    [field: SerializeField] public CharacterSO Character { get; private set; }

    public LiveStat Stats { get; private set; }
    public int HP
    {
        get { return _hp; }

        set
        {
            if (value < _hp)
            {
                PoolingManager.Instance.Spawn<DamagePopUpText>().Init(transform.position, _hp - value);
                Blink();
                if (value <= 0) CoDie();
            }

            _hp = value;
            onHpUpdated?.Invoke((float)_hp / Stats[(int)StatType.Hp]);
        }
    }
    // TODO :
    // change to list?
    public Dictionary<int, LiveSkill> SkillDic { get; private set; } = new Dictionary<int, LiveSkill>();
    // TODO :
    // change to list?
    public Dictionary<int, LiveItem> ItemDic { get; private set; } = new Dictionary<int, LiveItem>();
    public List<LivePotion> Potions { get; private set; } = new List<LivePotion>();
    public int Coin { get; private set; }

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private SpriteMask _sm;
    [SerializeField] private SpriteRenderer _blinkSr;
    private int _hp;
    private Coroutine _blinkCor;

    public event UnityAction<float> onHpUpdated;

    protected override void Awake()
    {
        base.Awake();

        Stats = new LiveStat(Character.Stats);

        // set main camera as a child
        //MainCamera.Camera.transform.SetParent(transform);
    }

    private void Start()
    {
        // initialize values
        HP = Stats[(int)StatType.Hp];
        _sr.sprite = Character.Sprite;
        _sm.sprite = Character.Sprite;
    }

    private void OnEnable()
    {
        JoystickEventChannelSO joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
        joystickEventChannel.onBeginDrag += Move;
        joystickEventChannel.onDrag += Move;

        PlayEventChannelSO playEventChannel = EventChannelFactory.Get<PlayEventChannelSO>();
        playEventChannel.onWaveStarted += OnWaveStarted;
        playEventChannel.onWaveFinished += OnWaveFinished;
    }

    private void OnDisable()
    {
        JoystickEventChannelSO joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
        joystickEventChannel.onBeginDrag -= Move;
        joystickEventChannel.onDrag -= Move;

        PlayEventChannelSO playEventChannel = EventChannelFactory.Get<PlayEventChannelSO>();
        playEventChannel.onWaveStarted -= OnWaveStarted;
        playEventChannel.onWaveFinished -= OnWaveFinished;
    }

    private void Move(Vector2 dir)
    {
        // move
        transform.Translate(dir.normalized * Stats[(int)StatType.Speed] * Time.deltaTime);

        // flip sprite
        _sr.transform.rotation = Quaternion.Euler(new Vector3(0, dir.x > 0 ? 0 : 180, 0));
    }

    public IEnumerator CoDie()
    {
        Debug.Log("Game Over");
        yield return null;
    }

    public void Blink()
    {
        if (_blinkCor == null) _blinkCor = StartCoroutine(CoBlink());
    }

    public IEnumerator CoBlink()
    {
        _sm.enabled = true;
        _blinkSr.gameObject.SetActive(true);
        yield return WaitForSecondsFactory.Get(0.1f);

        _sm.enabled = false;
        _blinkSr.gameObject.SetActive(false);
        _blinkCor = null;
    }

    public void Equip(LiveSkill liveSkill)
    {
        SkillDic.Add(liveSkill.Skill.Id, liveSkill);
    }

    public void Equip(LiveItem liveItem)
    {
        ItemDic.Add(liveItem.Item.Id, liveItem);
        Stats.Buffed(liveItem.Item.Buff, liveItem.Level);
    }

    public void Equip(LivePotion livePotion)
    {
        Potions.Add(livePotion);
        Stats.Buffed(livePotion.Potion.Buff);
    }

    public void Release(LivePotion livePotion)
    {
        Potions.Remove(livePotion);
        Stats.Debuffed(livePotion.Potion.Buff);
    }

    private void OnWaveStarted()
    {
        // start to invoking skills
        foreach (var skill in SkillDic.Values)
        {
            skill.Invoke();
        }
    }

    private void OnWaveFinished()
    {
        for (int i = 0; i < Potions.Count; i++)
        {
            Potions[i].Duration--;
            if (Potions[i].Duration == 0) Release(Potions[i]);
        }
    }
}
