using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration
{
    public static void Vibrate()
    {
        if (GameManager.Instance.User.Vibration) Handheld.Vibrate();
    }
}
