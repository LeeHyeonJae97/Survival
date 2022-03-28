using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int HP { get; set; }

    public IEnumerator CoDie();
    public void Blink();
    public IEnumerator CoBlink();
}
