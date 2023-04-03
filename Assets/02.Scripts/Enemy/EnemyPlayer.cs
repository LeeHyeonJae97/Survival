using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour, IDamageable, IQuadtreeObject
{
    public const float MIN_SPEED = 0.1f;

    public EnemySO Enemy
    {
        get { return _enemy; }

        set
        {
            _enemy = value;

            Hp = _enemy.Stats[(int)StatType.Hp].Value;
            Speed = _enemy.Stats[(int)StatType.Speed].Value;
            Color = _defaultColor;
            _sr.sprite = _enemy.Sprite;
            _sm.sprite = _enemy.Sprite;
            Movement = _enemy.Movement;

            Movement.Movement_Start(this);
        }
    }
    public int Hp
    {
        get { return _hp; }

        set
        {
            if (value < _hp)
            {
                PoolingManager.GetInstance().Spawn<DamagePopUpText>().Init(transform.position, _hp - value, Color.yellow);
                Blink();
                if (value <= 0) Die();
            }

            _hp = value;
        }
    }
    public float Speed
    {
        get { return _speed + MIN_SPEED; }

        set { _speed = Mathf.Max(value, MIN_SPEED); }
    }
    public float Sight { get { return _sight; } }
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
            //_sr.transform.rotation = Quaternion.Euler(new Vector3(0, _direction.x > 0 ? 0 : 180, 0));
        }
    }
    public Color Color
    {
        set { if (_sr.color == _defaultColor) _sr.color = value == default ? _defaultColor : value; }
    }
    public Color BlinkColor { set { _blinkSr.color = value; } }
    public Dictionary<SkillPropertyType, IEnumerator> CrowdControlCorDic { get; private set; } = new Dictionary<SkillPropertyType, IEnumerator>();
    public Dictionary<CrowdControlType, CrowdControl> CrowdControlDic { get; private set; } = new Dictionary<CrowdControlType, CrowdControl>();
    public Bounds Bounds { get { return _sr.bounds; } }

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private SpriteMask _sm;
    [SerializeField] private SpriteRenderer _blinkSr;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private int _coin;
    [SerializeField] private int _hpRecoveryPercent;
    [SerializeField] private float _sight;
    private int _hp;
    private float _speed;
    private EnemySO _enemy;
    private EnemyMovement _movement;
    private Vector2 _direction;
    private Coroutine _blinkCor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.GetComponentInParent<Player>().Hp -= Enemy.Stats[(int)StatType.Hp].Value;
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

    public void Move(List<EnemyPlayer> neighbors)
    {
        Movement.Movement_Update(this, neighbors);
    }

    public void Die()
    {
        // drop coins
        int count = _coin * (Enemy.Level + 1);

        for (int i = 0; i < count; i++)
        {
            PoolingManager.GetInstance().Spawn<Coin>().Init(transform.position);
        }

        // drop hp recovery
        if (Random.Range(0, 100) < _hpRecoveryPercent)
        {
            PoolingManager.GetInstance().Spawn<HpRecovery>().Init(transform.position);
        }

        // return WaitForSecondsFactory.Get(0.15f);
        Despawn();
    }

    public void Despawn()
    {
        // despawn
        WaveManager.GetInstance().Despawn(this);
    }

    public void Blink()
    {
        if (_blinkCor == null) _blinkCor = StartCoroutine(CoBlink());
    }

    public IEnumerator CoBlink()
    {
        _sm.enabled = true;
        _blinkSr.gameObject.SetActive(true);
        yield return WaitForSecondsFactory.GetPlayTime(0.1f);

        _sm.enabled = false;
        _blinkSr.gameObject.SetActive(false);
        _blinkCor = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireRect(Bounds.center, Bounds.size);
    }
}
