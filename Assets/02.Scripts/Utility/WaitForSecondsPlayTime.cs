using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsPlayTime : IEnumerator
{
    public object Current => null;
    public float Seconds { get { return _seconds; } }
    public bool IsDone { get; private set; }

    private float _seconds;
    private float _elapsed;

    public WaitForSecondsPlayTime(float seconds)
    {
        IsDone = false;
        _seconds = seconds;
        _elapsed = 0;
    }

    public bool MoveNext()
    {
        _elapsed += PlayTime.deltaTime;

        if (_elapsed >= _seconds)
        {
            IsDone = true;
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
        IsDone = false;
        _elapsed = 0;
    }
}
