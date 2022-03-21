using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int HP
    {
        get { return _hp; }

        set
        {
            _hp = value;
            if (_hp <= 0) Die();
        }
    }

    private int _hp;

    private void Update()
    {
        transform.Translate((Player.Instance.transform.position - transform.position) * Time.deltaTime);
    }

    public void Die()
    {
        EnemySpawner.Instance.Despawn(this);
    }
}
