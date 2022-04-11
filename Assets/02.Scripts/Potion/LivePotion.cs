using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePotion
{
    public int Duration { get; set; }
    public Potion Potion { get; private set; }

    public LivePotion(Potion potion)
    {
        Duration = Potion.Info.Duration;
        Potion = potion;
    }
}
