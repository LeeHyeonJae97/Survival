using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePotion
{
    public int Duration { get; set; }
    public PotionSO Potion { get; private set; }

    public LivePotion(PotionSO potion)
    {
        Duration = Potion.Duration;
        Potion = potion;
    }
}
