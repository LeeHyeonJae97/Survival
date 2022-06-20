using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOverSlot : MonoBehaviour
{
    [SerializeField] private Image _spriteImage;

    public void Init(Sprite sprite)
    {
        _spriteImage.sprite = sprite;
    }
}
