using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : Object
{
    public static T GetInstance(bool instantiateIfNot = true)
    {
        if (instantiateIfNot && _instance == null) _instance = Instantiate(Resources.Load<T>(typeof(T).ToString()));
        return _instance;
    }

    private static T _instance;

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this as T;
    }
}
