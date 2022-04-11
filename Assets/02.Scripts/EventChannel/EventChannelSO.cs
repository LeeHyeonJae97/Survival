using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannelSO : ScriptableObject
{
    public const string FILE_NAME = "EventChannel ";
    public const string MENU_NAME = "ScriptableObject/EventChannel/";

    [SerializeField] protected bool _log;
}
