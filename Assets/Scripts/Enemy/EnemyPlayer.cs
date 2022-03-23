using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO :
// need to separate playable enemy and data (like player and character)

public class EnemyPlayer : MonoBehaviour, IDamagable
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

    private int _curHP;
    private float _curSpeed;
    // TODO :
    // remove serializefield attribute
    [SerializeField] private Enemy _enemy;

    private void Update()
    {
        _enemy.Movement.Movement_Update(this);
    }

    public void Init()
    {
        // initialize values
        HP = _enemy.HP;
        Speed = _enemy.Speed;

        //
        _enemy.Movement.Movement_Start(this);

        //
        // _enemy = (parameter)
    }

    public void Die()
    {
        EnemySpawner.Instance.Despawn(this);
    }
}
