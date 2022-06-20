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

    public void Init(Vector2 position, int damage, Color color)
    {
        // set text
        _damageText.text = $"{damage}";

        // set color
        _damageText.color = color;

        // TODO :
        // balance value that is multiplied with Random.insideUnitCircle

        // drop
        transform.position = position + Random.insideUnitCircle * 1f;
        transform.DOMoveY(transform.position.y + _floatingDst, _duration).SetEase(Ease.Linear);

        // fade
        _damageText.alpha = 1;
        _damageText.DOFade(0, _duration).SetEase(Ease.InCubic)
            .onComplete += () => PoolingManager.GetInstance().Despawn<DamagePopUpText>(this);
    }
}
