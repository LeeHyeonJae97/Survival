using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour, IDamageable
{
    public const float MIN_SPEED = 0.1f;

    public EnemySO Enemy
    {
        get { return _enemy; }

        set
        {
            _enemy = value;

            HP = _enemy.Stats[(int)StatType.Hp].Value;
            Speed = _enemy.Stats[(int)StatType.Speed].Value;
            _sr.sprite = _enemy.Sprite;
            _sm.sprite = _enemy.Sprite;
            Movement = _enemy.Movement;

            Movement.Movement_Start(this);
        }
    }
    public int HP
    {
        get { return _hp; }

        set
        {
            if (value < _hp) Blink();
            _hp = value;
            if (_hp <= 0) StartCoroutine(CoDie());
        }
    }
    public float Speed
    {
        get { return _speed + MIN_SPEED; }

        set { _speed = Mathf.Max(value, MIN_SPEED); }
    }
    public EnemyMovement Movement
    {
        get { return _movement; }

        set
        {
            _movement = value;
            _movement.Movement_OnEanble(this);
        }
    }
    public Vector2 Direction
    {
        get { return _direction; }

        set
        {
            _direction = value.normalized;

            // flip sprite
            _sr.transform.rotation = Quaternion.Euler(new Vector3(0, _direction.x > 0 ? 0 : 180, 0));
        }
    }
    public Dictionary<SkillPropertyType, IEnumerator> CrowdControlCorDic { get; private set; } = new Dictionary<SkillPropertyType, IEnumerator>();
    public Dictionary<CrowdControlType, CrowdControl> CrowdControlDic { get; private set; } = new Dictionary<CrowdControlType, CrowdControl>();
    public Transform CrowdControlIconsHolder => _crowdControlIconsHolder;

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private SpriteMask _sm;
    [SerializeField] private SpriteRenderer _blinkSr;
    [SerializeField] private Transform _crowdControlIconsHolder;
    private int _hp;
    private float _speed;
    private EnemySO _enemy;
    private EnemyMovement _movement;
    private Vector2 _direction;
    private Coroutine _blinkCor;

    private void Update()
    {
        Movement.Movement_Update(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.GetComponentInParent<Player>().HP -= Enemy.Stats[(int)StatType.Hp].Value;
    }

    public void Init(EnemySO enemy)
    {
        // initialize values
        Enemy = enemy;

        // reset
        _sm.enabled = false;
        _blinkSr.gameObject.SetActive(false);
        _blinkCor = null;
    }

    public IEnumerator CoDie()
    {
        // drop coins
        for (int i = 0; i < Enemy.Coin; i++)
        {
            PoolingManager.Instance.Spawn<Coin>().Init(transform.position);
        }

        yield return WaitForSecondsFactory.Get(0.15f);
        Despawn();
    }

    public void Despawn()
    {
        // despawn
        EnemySpawner.Instance.Despawn(this);
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
}
