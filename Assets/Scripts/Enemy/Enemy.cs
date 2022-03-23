using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO :
// need to separate playable enemy and data (like player and character)

public class Enemy : MonoBehaviour, IDamagable
{
    public const float MIN_SPEED = 0.1f;

    public int HP
    {
        get { return _curHP; }

        set
        {
            _curHP = value;
            if (_curHP <= 0) Die();
        }
    }
    // TODO :
    // need to tell whether 'stiff' or 'slow down'
    public float Speed { get { return _curSpeed + MIN_SPEED; } set { _curSpeed = value; } }
    public Dictionary<SkillPropertyType, IEnumerator> CrowdControlCorDic { get; private set; } = new Dictionary<SkillPropertyType, IEnumerator>();

    [SerializeField] private int _hp;
    [SerializeField] private float _speed;
    [SerializeField] private EnemyMovement _movement;
    private int _curHP;
    private float _curSpeed;

    private void Update()
    {
        _movement.Movement_Update(this);
    }

    public void Init()
    {
        // initialize values
        HP = _hp;
        Speed = _speed;

        //
        _movement.Movement_Start(this);
    }

    public void Die()
    {
        EnemySpawner.Instance.Despawn(this);
    }
}
