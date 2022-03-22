using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsFactory
{
    public static Dictionary<float, Queue<WaitForSeconds>> _dic = new Dictionary<float, Queue<WaitForSeconds>>();

    public static WaitForSeconds Get(float seconds)
    {
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

    public static void Return(WaitForSeconds wfs)
    {
        if (_dic.TryGetValue(wfs.Seconds, out Queue<WaitForSeconds> queue)) queue.Enqueue(wfs);
    }
}
