using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private Image _hpFillImage;
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnEnable()
    {
        _player.onHpUpdated += OnHpUpdated;
    }

    private void OnDisable()
    {
        _player.onHpUpdated -= OnHpUpdated;
    }

    private void OnHpUpdated(float ratio)
    {
        _hpFillImage.fillAmount = ratio;
    }
}
