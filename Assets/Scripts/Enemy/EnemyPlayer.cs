using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO :
// need to separate playable enemy and data (like player and character)

public class EnemyPlayer : MonoBehaviour, IDamagable
{
    public const float MIN_SPEED = 0.1f;

    public Enemy Enemy
    {
        get { return _enemy; }

        set
        {
            _enemy = value;

            HP = _enemy.HP;
            Speed = _enemy.Speed;
            Movement = _enemy.Movement;

            Movement.Movement_Start(this);
        }
    }
    public int HP
    {
        get { return _curHP; }

        set
        {
            _curHP = value;
            if (_curHP <= 0) Die();
        }
    }
    public float Speed
    {
        get { return _curSpeed + MIN_SPEED; }
        set { _curSpeed = value; }
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
    public Dictionary<SkillPropertyType, IEnumerator> CrowdControlCorDic { get; private set; } = new Dictionary<SkillPropertyType, IEnumerator>();

    private Enemy _enemy;
    // TODO :
    // remove serializefield attribute
    [SerializeField] private int _curHP;
    private float _curSpeed;
    // TODO :
    // remove serializefield attribute
    [SerializeField] private EnemyMovement _movement;

    private void Update()
    {
        Movement.Movement_Update(this);
    }

    public void Init(Enemy enemy)
    {
        // initialize values
        Enemy = enemy;
    }

    public void Die()
    {
        EnemySpawner.Instance.Despawn(this);
    }
}
