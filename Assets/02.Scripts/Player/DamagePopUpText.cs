using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpText : MonoBehaviour
{
    // TODO :
    // balance duration and floating distance
    [SerializeField] private float _duration;
    [SerializeField] private float _floatingDst;
    private TextMeshPro _damageText;

    private void Awake()
    {
        _damageText = GetComponent<TextMeshPro>();
    }

    public void Init(Vector2 position, int damage)
    {
        // set text
        _damageText.text = $"{damage}";

        // TODO :
        // balance value that is multiplied with Random.insideUnitCircle

        // drop
        transform.position = position + Random.insideUnitCircle * 1f;
        transform.DOMoveY(transform.position.y + _floatingDst, _duration).SetEase(Ease.Linear);

        // fade
        _damageText.alpha = 1;
        _damageText.DOFade(0, _duration).SetEase(Ease.InCubic)
            .onComplete += () => PoolingManager.Instance.Despawn<DamagePopUpText>(this);
    }
}
