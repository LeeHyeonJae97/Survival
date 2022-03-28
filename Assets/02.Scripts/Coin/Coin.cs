using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Vector2 _speed;
    private float _curSpeed;

    public void Init(Vector3 position)
    {
        transform.position = position + (Vector3)Random.insideUnitCircle;
        _curSpeed = _speed.y;
    }

    private void Update()
    {
        Vector3 dir = Player.Instance.transform.position - transform.position;

        if (dir.sqrMagnitude < 0.01f)
        {
            PoolingManager.Instance.Despawn<Coin>(this);
        }
        else
        {
            transform.Translate(dir.normalized * _curSpeed * Time.deltaTime);
            _curSpeed = Mathf.Max(_curSpeed -= Time.deltaTime, _speed.x);
        }
    }
}
