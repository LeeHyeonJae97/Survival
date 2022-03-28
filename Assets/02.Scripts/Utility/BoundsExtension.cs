using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsExtension
{
    public static Vector2 OnRandom(this Bounds bounds)
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return new Vector2(bounds.min.x, Mathf.Lerp(bounds.min.y, bounds.max.y, Random.value));

            case 1:
                return new Vector2(bounds.max.x, Mathf.Lerp(bounds.min.y, bounds.max.y, Random.value));

            case 2:
                return new Vector2(Mathf.Lerp(bounds.min.x, bounds.max.x, Random.value), bounds.min.y);

            case 3:
                return new Vector2(Mathf.Lerp(bounds.min.x, bounds.max.x, Random.value), bounds.max.y);

            default:
                return Vector2.zero;
        }
    }
}
