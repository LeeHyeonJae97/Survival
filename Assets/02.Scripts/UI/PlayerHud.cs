using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private Image _hpFillImage;

    private void OnEnable()
    {
        Player.Instance.onHpUpdated += OnHpUpdated;
    }

    private void OnDisable()
    {
        Player.Instance.onHpUpdated -= OnHpUpdated;
    }

    private void OnHpUpdated(float ratio)
    {
        _hpFillImage.fillAmount = ratio;
    }
}
