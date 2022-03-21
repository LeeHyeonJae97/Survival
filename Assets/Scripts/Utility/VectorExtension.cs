using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static float SqrDst(this Vector3 vec, Vector3 point)
    {
        return (vec - point).sqrMagnitude;
    }
}
