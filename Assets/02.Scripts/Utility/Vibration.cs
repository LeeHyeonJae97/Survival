using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration
{
    public static void Vibrate()
    {
        if (GameManager.GetInstance().User.Vibration) Handheld.Vibrate();
    }
}
