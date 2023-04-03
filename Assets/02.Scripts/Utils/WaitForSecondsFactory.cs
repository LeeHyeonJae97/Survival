using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsFactory
{
    public static Dictionary<float, Queue<WaitForSeconds>> _dic = new Dictionary<float, Queue<WaitForSeconds>>();
    public static Dictionary<float, Queue<WaitForSecondsPlayTime>> _playTimeDic = new Dictionary<float, Queue<WaitForSecondsPlayTime>>();

    public static WaitForSeconds Get(float seconds)
    {
        if (seconds <= 0) return null;

        if (!_dic.TryGetValue(seconds, out Queue<WaitForSeconds> queue))
        {
            queue = new Queue<WaitForSeconds>();
            _dic.Add(seconds, queue);
        }

        if (queue.Count > 0)
        {
            WaitForSeconds wfs = queue.Dequeue();
            wfs.Reset();
            return wfs;
        }
        else
        {
            return new WaitForSeconds(seconds);
        }
    }

    public static WaitForSecondsPlayTime GetPlayTime(float seconds)
    {
        if (seconds <= 0) return null;

        if (!_playTimeDic.TryGetValue(seconds, out Queue<WaitForSecondsPlayTime> queue))
        {
            queue = new Queue<WaitForSecondsPlayTime>();
            _playTimeDic.Add(seconds, queue);
        }

        if (queue.Count > 0)
        {
            WaitForSecondsPlayTime wfs = queue.Dequeue();
            wfs.Reset();
            return wfs;
        }
        else
        {
            return new WaitForSecondsPlayTime(seconds);
        }
    }

    public static void Return(WaitForSeconds wfs)
    {
        if (_dic.TryGetValue(wfs.Seconds, out Queue<WaitForSeconds> queue)) queue.Enqueue(wfs);
    }

    public static void Return(WaitForSecondsPlayTime wfs)
    {
        if (_playTimeDic.TryGetValue(wfs.Seconds, out Queue<WaitForSecondsPlayTime> queue)) queue.Enqueue(wfs);
    }
}
