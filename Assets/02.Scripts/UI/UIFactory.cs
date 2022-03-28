using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFactory
{
    private static Dictionary<string, UI> _dic = new Dictionary<string, UI>();

    public static void Add(UI ui)
    {
        _dic.Add(ui.GetType().ToString(), ui);
    }

    public static void Remove(UI ui)
    {
        _dic.Remove(ui.GetType().ToString());
    }

    public static T Get<T>() where T : UI
    {
        string type = typeof(T).ToString();

        if (!_dic.TryGetValue(type, out UI ui))
        {
            Debug.LogError($"There's no UI : {type}");
        }
        return (T)ui;
    }
}
