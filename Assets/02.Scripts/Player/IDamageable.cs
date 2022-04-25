using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int Hp { get; set; }

    public void Die();
    public void Blink();
    public IEnumerator CoBlink();
}
