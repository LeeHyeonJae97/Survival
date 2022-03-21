using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>, IDamagable
{
    // TODO :
    // need to be static
    [SerializeField] private Character _character;

    public int HP
    {
        get { return _hp; }

        set
        {
            _hp = value;
            if (_hp <= 0) Die();
        }
    }
    // TODO :
    // delete serializing
    [field: SerializeField] public List<Skill> Skills { get; set; } = new List<Skill>();
    private JoystickEventChannelSO JoystickEventChannel
    {
        get
        {
            if (_joystickEventChannel == null) _joystickEventChannel = EventChannelFactory.Get<JoystickEventChannelSO>();
            return _joystickEventChannel;
        }
    }

    [SerializeField] private float _dashDst;
    [SerializeField] private float _dashDuration;
    private int _hp;
    private bool _dash;
    private JoystickEventChannelSO _joystickEventChannel;
    private CircleCollider2D _coll;

    protected override void Awake()
    {
        base.Awake();

        // get components
        _coll = GetComponentInChildren<CircleCollider2D>();

        // initialize values
        HP = _character.HP;
    }

    private void OnEnable()
    {
        JoystickEventChannel.onBeginDrag += Move;
        JoystickEventChannel.onDrag += Move;
        JoystickEventChannel.onBoundaryDoubleClicked += Dash;
    }

    private void OnDisable()
    {
        JoystickEventChannel.onBeginDrag -= Move;
        JoystickEventChannel.onDrag -= Move;
        JoystickEventChannel.onBoundaryDoubleClicked -= Dash;
    }

    private void Move(Vector2 dir)
    {
        // cannot move during dash
        if (_dash) return;

        transform.Translate(dir.normalized * _character.MoveSpeed * Time.deltaTime);
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
}
