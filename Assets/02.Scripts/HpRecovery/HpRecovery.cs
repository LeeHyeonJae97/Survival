using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : MonoBehaviour
{
    private const float VANISH_DST_THRESHOLD = 1f;

    [SerializeField] private Vector2 _speed;
    private float _curSpeed;

    public void Init(Vector3 position)
    {
        transform.position = position;
        _curSpeed = _speed.x;
    }

    private void Update()
    {
        // NOTICE :
        // maybe need to cachee 'PlayInfoUI'

        Vector2 target = Player.GetInstance().transform.position;

        if ((target - (Vector2)transform.position).sqrMagnitude < VANISH_DST_THRESHOLD)
        {
            Player.GetInstance().Hp += (int)(Player.GetInstance().Stats[(int)StatType.Hp] * 0.2f);
            PoolingManager.GetInstance().Despawn<HpRecovery>(this);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, _curSpeed * PlayTime.deltaTime);
            _curSpeed = Mathf.Min(_curSpeed += PlayTime.deltaTime, _speed.y);
        }
    }
}
