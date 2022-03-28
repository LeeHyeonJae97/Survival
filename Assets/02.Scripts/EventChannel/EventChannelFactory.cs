using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChannelFactory
{
    private static Dictionary<string, EventChannelSO> Dic
    {
        get
        {
            if (_dic == null)
            {
                var channels = Resources.LoadAll<EventChannelSO>("EventChannel");

                _dic = new Dictionary<string, EventChannelSO>();
                for (int i = 0; i < channels.Length; i++)
                {
                    _dic.Add(channels[i].GetType().ToString(), channels[i]);
                }
            }
            return _dic;
        }
    }
    private static Dictionary<string, EventChannelSO> _dic;

    public static T Get<T>() where T : EventChannelSO
    {
        string type = typeof(T).ToString();

        if (!Dic.TryGetValue(type, out EventChannelSO channel))
        {
            Debug.LogError($"There's no EventChannel : {type}");
        }
        return (T)channel;
    }
}
