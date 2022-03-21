using System.Collections;
using UnityEngine;

public class WaitForSeconds : IEnumerator
{
    public object Current => null;
    public float Seconds => _seconds;

    private float _seconds;
    private float _elapsed;

    public WaitForSeconds(float seconds)
    {
        _seconds = seconds;
        _elapsed = 0;
    }

    public bool MoveNext()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed >= _seconds)
        {
            Reset();
            WaitForSecondsFactory.Return(this);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Reset()
    {
        _elapsed = 0;
    }
}