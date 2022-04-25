using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : SingletonMonoBehaviour<Player>, IDamageable
{
    public Character Character
    {
        get
        {
            if (_character == null) _character = CharacterFactory.Get(GameManager.Instance.User.EquippedCharacterId);
            return _character;
        }
    }
    public LiveStat Stats { get; private set; }
    public int Hp
    {
        get { return _hp; }

        set
        {
            if (value < _hp)
            {
                // spawn damage text
                PoolingManager.Instance.Spawn<DamagePopUpText>().Init(transform.position, _hp - value, Color.red);

                // blink
                Blink();

                // check die
                if (_hp > 0 && value <= 0) Die();

                // shake camera
                MainCamera.Shake();
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
    public int Coin
    {
        get { return _coin; }

        set
        {
            _coin = value;
            onCoinUpdated?.Invoke(_coin);
        }
    }

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private SpriteMask _sm;
    [SerializeField] private SpriteRenderer _blinkSr;
    private Character _character;
    private int _hp;
    private int _coin;
    private Coroutine _blinkCor;

    public event UnityAction<float> onHpUpdated;
    public event UnityAction<int> onCoinUpdated;

    private void Start()
    {
        Stats = new LiveStat(Character.Info.Stats[Character.Reinforced]);

        // initialize values
        Hp = Stats[(int)StatType.Hp];
        Coin = PlayManager.INITIAL_COIN;
        _sr.sprite = Character.Info.Sprite;
        _sm.sprite = Character.Info.Sprite;

        // set camera's following target
        MainCamera.Target = transform;
    }

    private void OnDestroy()
    {
        MainCamera.Target = null;
    }

    private void OnEnable()
    {
        JoystickEventChannelSO joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
        joystickEventChannel.onBeginDrag += Move;
        joystickEventChannel.onDrag += Move;

        PlayEventChannelSO playEventChannel = EventChannelFactory.Get<PlayEventChannelSO>();
        playEventChannel.OnWaveStarted += OnWaveStarted;
        playEventChannel.OnWaveFinished += OnWaveFinished;
    }

    private void OnDisable()
    {
        JoystickEventChannelSO joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
        joystickEventChannel.onBeginDrag -= Move;
        joystickEventChannel.onDrag -= Move;

        PlayEventChannelSO playEventChannel = EventChannelFactory.Get<PlayEventChannelSO>();
        playEventChannel.OnWaveStarted -= OnWaveStarted;
        playEventChannel.OnWaveFinished -= OnWaveFinished;
    }

    private void Update()
    {
        MainCamera.Follow();
    }

    private void Move(Vector2 dir)
    {
        // move
        transform.Translate(dir.normalized * Stats[(int)StatType.Speed] * PlayTime.deltaTime);

        // flip sprite
        _sr.transform.rotation = Quaternion.Euler(new Vector3(0, dir.x > 0 ? 0 : 180, 0));
    }

    public void Die()
    {
        EventChannelFactory.Get<PlayEventChannelSO>().FinishPlay();
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

    public void Equip(LiveItem liveItem)
    {
        ItemDic.Add(liveItem.Item.Info.Id, liveItem);
        Stats.Buffed(liveItem.Item.Info.Buffs[liveItem.Level].Buffs[liveItem.Item.Reinforced]);

        // register on illustrated book
        liveItem.Item.Register();
    }

    public void Equip(LiveSkill liveSkill)
    {
        SkillDic.Add(liveSkill.Skill.Info.Id, liveSkill);

        // register on illustrated book
        liveSkill.Skill.Register();
    }

    public void Equip(LivePotion livePotion)
    {
        Potions.Add(livePotion);
        Stats.Buffed(livePotion.Potion.Info.Buff);

        // register on illustrated book
        livePotion.Potion.Register();
    }

    public void Release(LivePotion livePotion)
    {
        Potions.Remove(livePotion);
        Stats.Debuffed(livePotion.Potion.Info.Buff);
    }

    private void OnWaveStarted()
    {
        StopAllCoroutines();

        // start to invoking skills
        foreach (var skill in SkillDic.Values)
        {
            skill.Invoke(this);
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
