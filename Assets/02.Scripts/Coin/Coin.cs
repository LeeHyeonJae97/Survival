using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private const float VANISH_DST_THRESHOLD = 1f;

    [SerializeField] private Vector2 _speed;
    private float _curSpeed;

    public void Init(Vector3 position)
    {
        transform.position = position + (Vector3)Random.insideUnitCircle;
        _curSpeed = _speed.x;
    }

    private void Update()
    {
        // NOTICE :
        // maybe need to cachee 'PlayInfoUI'

        Vector2 target = UIFactory.Get<PlayInfoUI>().CoinImagePosition;

        if ((target - (Vector2)transform.position).sqrMagnitude < VANISH_DST_THRESHOLD)
        {
            Player.Instance.Coin += 1;
            PoolingManager.Instance.Despawn<Coin>(this);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, _curSpeed * Time.deltaTime);
            _curSpeed = Mathf.Min(_curSpeed += Time.deltaTime, _speed.y);
        }
    }
}
