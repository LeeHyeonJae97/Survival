using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayTime
{
    public static float timeScale
    {
        get { return _timeScale * Time.timeScale; }

        set { _timeScale = value; }
    }

    public static float deltaTime { get { return _timeScale * Time.deltaTime; } }

    private static float _timeScale;
}
