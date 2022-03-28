using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : SingletonMonoBehaviour<Player>, IDamageable
{
    // TODO :
    // need to be static
    [SerializeField] private Character _character;

    public int HP
    {
        get { return _hp; }

        set
        {
            if (value < _hp)
            {
                PoolingManager.Instance.Spawn<DamagePopUpText>().Init(transform.position, _hp - value);
                Blink();
                if (value <= 0) Die();
            }

            _hp = value;
            onHpUpdated?.Invoke((float)_hp / _character.HP);
        }
    }
    public List<Skill> Skills { get; set; } = new List<Skill>();

    [SerializeField] private CircleCollider2D _coll;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private SpriteMask _sm;
    [SerializeField] private SpriteRenderer _blinkSr;
    [SerializeField] private float _dashDst;
    [SerializeField] private float _dashDuration;
    private int _hp;
    private bool _dash;
    private JoystickEventChannelSO _joystickEventChannel;
    private Coroutine _blinkCor;

    public event UnityAction<float> onHpUpdated;

    protected override void Awake()
    {
        base.Awake();

        _joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();

        // set main camera as a child
        //MainCamera.Camera.transform.SetParent(transform);
    }

    private void Start()
    {
        // initialize values
        HP = _character.HP;
        _sr.sprite = _character.Sprite;
        _sm.sprite = _character.Sprite;
    }

    private void OnEnable()
    {
        _joystickEventChannel.onBeginDrag += Move;
        _joystickEventChannel.onDrag += Move;
        _joystickEventChannel.onBoundaryDoubleClicked += Dash;
    }

    private void OnDisable()
    {
        _joystickEventChannel.onBeginDrag -= Move;
        _joystickEventChannel.onDrag -= Move;
        _joystickEventChannel.onBoundaryDoubleClicked -= Dash;
    }

    private void Move(Vector2 dir)
    {
        // cannot move during dash
        if (_dash) return;

        // move
        transform.Translate(dir.normalized * _character.MoveSpeed * Time.deltaTime);

        // flip sprite
        _sr.transform.rotation = Quaternion.Euler(new Vector3(0, dir.x > 0 ? 0 : 180, 0));
    }

    private void Dash(Vector2 dir)
    {
        _dash = true;

        // check if blocked by wall
        var hit = Physics2D.CircleCast(transform.position, _coll.radius, dir, _dashDst, 1 << LayerMask.NameToLayer("Wall"));

        // dash to the destination position
        Vector2 dest = hit.collider == null ? (Vector2)transform.position + dir.normalized * _dashDst : hit.centroid - dir.normalized * 0.1f;
        transform.DOMove(dest, _dashDuration).onComplete += () => _dash = false;
    }

    public void Die()
    {
        Debug.Log("Game Over");
    }

    public void Blink()
    {
        if (_blinkCor == null) _blinkCor = StartCoroutine(CoBlink());
    }

    public IEnumerator CoBlink()
    {
        _blinkSr.gameObject.SetActive(true);
        yield return WaitForSecondsFactory.Get(0.1f);
        _blinkSr.gameObject.SetActive(false);
        _blinkCor = null;
    }
}
